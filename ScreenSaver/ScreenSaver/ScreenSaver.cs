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
        /// The <see cref="Control"/> that shall be used as screen saver.
        /// </summary>
        private Control screenSaverControl;

        /// <summary>
        /// The <see cref="Control"/> that shall be used as preview.
        /// </summary>
        private Control previewControl;

        /// <summary>
        /// The <see cref="Control"/> that shall be used when the user configures the screen saver.
        /// </summary>
        private Control configurationControl;

        #endregion

        #region Constructors and Destructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Control"/> that shall be used as screen saver.
        /// </summary>
        /// <value>A <see cref="Control"/>.</value>
        public Control ScreenSaverControl
        {

        }

        /// <summary>
        /// Gets or sets the <see cref="Control"/> that shall be used as preview.
        /// </summary>
        /// <value>A <see cref="Control"/> or <c>null</c> if the <see cref="ScreenSaverControl"/> shall be used.</value>
        public Control PreviewControl
        {

        }

        /// <summary>
        /// Gets or sets the <see cref="Control"/> that shall be used when the user configures the screen saver.
        /// </summary>
        /// <value>A <see cref="Control"/> or <c>null</c> if this screen saver does not support configuration.</value>
        public Control ConfigurationControl
        {

        }

        #endregion

        #region Methods

        #endregion
    }
}
