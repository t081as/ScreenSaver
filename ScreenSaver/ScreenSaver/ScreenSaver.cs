#region MIT License
// The MIT License (MIT)
//
// Copyright © 2017-2018 Tobias Koch <t.koch@tk-software.de>
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

#region Namespaces
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
#endregion

namespace ScreenSaver
{
    /// <summary>
    /// Encapsulates the functionality of a screen saver for the Microsoft Windows operating system.
    /// </summary>
    public class ScreenSaver
    {
        #region Constants and Fields

        /// <summary>
        /// The configuration of the screen saver.
        /// </summary>
        private IScreenSaverConfiguration screenSaverConfiguration;

        /// <summary>
        /// Provides simple translations.
        /// </summary>
        private ScreenSaverTranslation translation;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSaver"/> class using the given screen saver configuration.
        /// </summary>
        /// <param name="screenSaverConfiguration">The configuration of the screen saver.</param>
        /// <exception cref="ArgumentNullException"><c>screenSaverConfiguration</c> is null.</exception>
        /// <exception cref="ArgumentNullException">A member of <c>screenSaverConfiguration</c> is null.</exception>
        public ScreenSaver(IScreenSaverConfiguration screenSaverConfiguration)
        {
            if (screenSaverConfiguration == null)
            {
                throw new ArgumentNullException("screenSaverConfiguration");
            }

            if (screenSaverConfiguration.ScreenSaverName == null)
            {
                throw new ArgumentNullException("IScreenSaverConfiguration.ScreenSaverName");
            }

            this.translation = new ScreenSaverTranslation(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            this.screenSaverConfiguration = screenSaverConfiguration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration of the screen saver.
        /// </summary>
        public IScreenSaverConfiguration ScreenSaverConfiguration
        {
            get
            {
                return this.screenSaverConfiguration;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Runs this application as a screen saver.
        /// </summary>
        /// <param name="args">The command line arguments given to the application.</param>
        /// <exception cref="ArgumentNullException"><c>args</c> is null.</exception>
        public void Run(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string command = string.Empty;

            if (args.Length > 0)
            {
                command = args[0].Trim().ToLower();
            }

            if (command.Length > 0)
            {
                command = command.Substring(0, 2);
            }

            switch (command)
            {
                case "/s":

                    this.Show();

                break;

                case "/p":

                    if (args.Length > 1)
                    {
                        int parentWindowHandle;

                        if (int.TryParse(args[1].Trim(), out parentWindowHandle))
                        {
                            this.Preview(new IntPtr(parentWindowHandle));
                        }
                        else
                        {
                            Environment.Exit(1);
                        }
                    }
                    else
                    {
                        Environment.Exit(1);
                    }

                break;

                case "/c":
                case "":

                    this.Configure();

                break;

                default:

                    Environment.Exit(1);
                    break;
            }
        }

        /// <summary>
        /// Shows the screen saver.
        /// </summary>
        protected virtual void Show()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                Control screenSaverControl = this.screenSaverConfiguration.CreateScreenSaverControl();

                if (screenSaverControl == null)
                {
                    throw new ArgumentNullException("IScreenSaverConfiguration.CreateScreenSaverControl()");
                }

                ScreenSaverForm form = new ScreenSaverForm(screenSaverControl, screen);
                form.Show();
            }

            Application.Run();
        }

        /// <summary>
        /// Shows the screen saver preview in the parent window provided by the operating system.
        /// </summary>
        /// <param name="parentWindowHandle">The handle of the parent window provided by the operating system.</param>
        protected virtual void Preview(IntPtr parentWindowHandle)
        {
            Control previewControl = this.screenSaverConfiguration.CreatePreviewControl();

            if (previewControl == null)
            {
                previewControl = this.screenSaverConfiguration.CreateScreenSaverControl();
            }

            if (previewControl != null)
            {
                PreviewForm form = new PreviewForm(previewControl);
                form.CreateControl();

                this.DockPreviewWindow(form, parentWindowHandle);

                Application.Run(form);
            }
            else
            {
                throw new ArgumentNullException("IScreenSaverConfiguration.CreateScreenSaverControl()");
            }
        }

        /// <summary>
        /// Configures the screen saver.
        /// </summary>
        protected virtual void Configure()
        {
            ConfigurationControl configurationControl = this.screenSaverConfiguration.CreateConfigurationControl();

            if (configurationControl != null)
            {
                ConfigurationForm form = new ConfigurationForm(this.translation, configurationControl);
                form.CreateControl();
                form.Text = this.screenSaverConfiguration.ScreenSaverName;

                configurationControl.LoadConfiguration();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    configurationControl.SaveConfiguration();
                }
            }
            else
            {
                MessageBox.Show(
                    this.translation.NoOptionsMessageTranslation,
                    this.screenSaverConfiguration.ScreenSaverName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Shows the screen saver preview in the area provided by the operating system.
        /// </summary>
        /// <param name="previewWindow">The screen saver preview window.</param>
        /// <param name="parentWindowHandle">The handle provided by the operating system.</param>
        protected virtual void DockPreviewWindow(Form previewWindow, IntPtr parentWindowHandle)
        {
            // Set new parent window
            if (NativeMethods.SetParent(previewWindow.Handle, parentWindowHandle) == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            // Change style of child window
            int windowStyle = NativeMethods.GetWindowLong(previewWindow.Handle, NativeMethods.GWL_STYLE);

            if (windowStyle == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            windowStyle = windowStyle | NativeMethods.WS_CHILD;

            if (NativeMethods.SetWindowLong(previewWindow.Handle, NativeMethods.GWL_STYLE, windowStyle) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            // Set position and size of child window
            Rectangle rect;

            if (NativeMethods.GetClientRect(parentWindowHandle, out rect) == false)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            previewWindow.Location = new Point(0, 0);
            previewWindow.Bounds = rect;
        }

        #endregion
    }
}
