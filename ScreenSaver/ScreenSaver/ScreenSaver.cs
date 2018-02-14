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

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSaver"/> class using the given screen saver configuration.
        /// </summary>
        /// <param name="screenSaverConfiguration">The configuration of the screen saver.</param>
        /// <exception cref="ArgumentNullException"><c>screenSaverConfiguration</c> is null.</exception>
        public ScreenSaver(IScreenSaverConfiguration screenSaverConfiguration)
        {
            if (screenSaverConfiguration == null)
            {
                throw new ArgumentNullException("screenSaverConfiguration");
            }

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
            throw new NotImplementedException();
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
                previewControl = this.screenSaverConfiguration.CreateScreenSaverControl(Screen.PrimaryScreen);
            }

            if (previewControl != null)
            {
                throw new NotImplementedException();
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
            Control configurationControl = this.screenSaverConfiguration.CreateConfigurationControl();

            if (configurationControl != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        #endregion
    }
}
