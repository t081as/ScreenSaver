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
using System.Windows.Forms;
#endregion

namespace ScreenSaver
{
    /// <summary>
    /// The <see cref="Form"/> hosting the configuration control.
    /// </summary>
    internal partial class ConfigurationForm : Form
    {
        #region Constants and Fields

        /// <summary>
        /// Provides simple translations.
        /// </summary>
        private ScreenSaverTranslation translation;

        /// <summary>
        /// The <see cref="ConfigurationControl"/> that shall be displayed.
        /// </summary>
        private ConfigurationControl configurationControl;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationForm"/> class.
        /// </summary>
        /// <param name="translation">The translation provider.</param>
        /// <param name="configurationControl">The <see cref="ConfigurationControl"/> that shall be displayed.</param>
        public ConfigurationForm(ScreenSaverTranslation translation, ConfigurationControl configurationControl)
        {
            this.InitializeComponent();
            this.translation = translation;
            this.configurationControl = configurationControl;

            this.Width = configurationControl.Width;
            this.Height = configurationControl.Height + this.flowLayoutPanel.Height;

            this.tableLayoutPanel.Controls.Add(configurationControl, 0, 0);
            configurationControl.Dock = DockStyle.Fill;

            this.buttonOk.Text = translation.OkButtonTranslation;
            this.buttonCancel.Text = translation.CancelButtonTranslation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration control.
        /// </summary>
        public ConfigurationControl ConfigurationControl
        {
            get
            {
                return this.configurationControl;
            }
        }

        #endregion
    }
}