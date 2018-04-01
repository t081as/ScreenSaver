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
using System.IO;
using System.Windows.Forms;
using ScreenSaver.IO;
#endregion

namespace ScreenSaver.Test
{
    /// <summary>
    /// The configuration of the test screen saver.
    /// </summary>
    internal class ScreenSaverTestConfiguration : IScreenSaverConfiguration
    {
        #region Constructors and Destructors

        /// <summary>
        /// Contains the configuration.
        /// </summary>
        private Configuration configuration;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSaverTestConfiguration"/> class.
        /// </summary>
        public ScreenSaverTestConfiguration()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), SimpleConfigurationControl.DefaultConfigurationFileName);

            try
            {
                this.configuration = Configuration.Load(fileName);
            }
            catch
            {
                this.configuration = new Configuration();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the screen saver.
        /// </summary>
        public string ScreenSaverName
        {
            get
            {
                return "Test";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates and returns a new instance of a <see cref="Control"/> used as a screen saver for the given screen.
        /// </summary>
        /// <returns>A <see cref="Control"/> used as a screen saver for the given screen.</returns>
        public Control CreateScreenSaverControl()
        {
            int screenSaverType = -1;

            try
            {
                screenSaverType = int.Parse(this.configuration.GetValue("type", "-1"));
            }
            catch
            {
                screenSaverType = -1;
            }

            switch (screenSaverType)
            {
                case 1:
                    return new VideoControl();

                default:
                    return new ColorControl();
            }
        }

        /// <summary>
        /// Creates and returns a new instance of a <see cref="Control"/> used as a preview of the screen saver.
        /// </summary>
        /// <returns>
        /// A <see cref="Control"/> used as a preview of the screen saver or <c>null</c> if the screen saver control
        /// of shall be used instead.
        /// </returns>
        public Control CreatePreviewControl()
        {
            return null;
        }

        /// <summary>
        /// Creates and returns a new instance of a <see cref="Control"/> used to configure the screen saver.
        /// </summary>
        /// <returns>
        /// A <see cref="Control"/> used to configure the screen saver or <c>null</c> if the screen saver
        /// can't be configured.
        /// </returns>
        public ConfigurationControl CreateConfigurationControl()
        {
            return new SimpleConfigurationControl();
        }

        #endregion
    }
}
