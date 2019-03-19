#region MIT License
// The MIT License (MIT)
//
// Copyright © 2017-2019 Tobias Koch <t.koch@tk-software.de>
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
using System.IO;
using System.Windows.Forms;
using ScreenSaver.IO;
#endregion

namespace ScreenSaver.Test
{
    /// <summary>
    /// Provides the configuration for the screen saver.
    /// </summary>
    public partial class SimpleConfigurationControl : ConfigurationControl
    {
        #region Constants and Fields

        /// <summary>
        /// The default name of the simple configuration file used for this test screen saver.
        /// </summary>
        public const string DefaultConfigurationFileName = ".screensavertest";

        /// <summary>
        /// The path and filename of the simple configuration file.
        /// </summary>
        private string configurationFileName;

        /// <summary>
        /// Contains the configuration.
        /// </summary>
        private Configuration configuration;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConfigurationControl"/> class.
        /// </summary>
        public SimpleConfigurationControl()
        {
            this.InitializeComponent();
            this.configurationFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), DefaultConfigurationFileName);

            try
            {
                this.configuration = Configuration.Load(this.configurationFileName);
            }
            catch
            {
                this.configuration = new Configuration();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The application shall load and display stored configuration values.
        /// </summary>
        public override void LoadConfiguration()
        {
            try
            {
                this.comboBoxType.SelectedIndex = int.Parse(this.configuration.GetValue("type", "-1"));
            }
            catch
            {
                this.comboBoxType.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// The aplication shall store configuration values.
        /// </summary>
        public override void SaveConfiguration()
        {
            Configuration configuration = new Configuration();
            configuration.SetValue("type", this.comboBoxType.SelectedIndex.ToString());

            Configuration.Save(this.configurationFileName, configuration);
        }

        #endregion
    }
}
