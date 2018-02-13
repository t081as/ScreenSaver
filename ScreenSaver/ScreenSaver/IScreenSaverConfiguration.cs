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
    /// Describes an object that configures a screen saver.
    /// </summary>
    public interface IScreenSaverConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets the name of the screen saver.
        /// </summary>
        string ScreenSaverName
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates and returns a new instance of a <see cref="Control"/> used as a screen saver for the given screen.
        /// </summary>
        /// <param name="screen">The screen that will host the <see cref="Control"/>.</param>
        /// <returns>A <see cref="Control"/> used as a screen saver for the given screen.</returns>
        Control CreateScreenSaverControl(Screen screen);

        /// <summary>
        /// Creates and returns a new instance of a <see cref="Control"/> used as a preview of the screen saver.
        /// </summary>
        /// <returns>
        /// A <see cref="Control"/> used as a preview of the screen saver or <c>null</c> if the screen saver control
        /// of the primary screen shall be used instead.
        /// </returns>
        Control CreatePreviewControl();

        /// <summary>
        /// Creates and returns a new instance of a <see cref="Control"/> used to configure the screen saver.
        /// </summary>
        /// <returns>
        /// A <see cref="Control"/> used to configure the screen saver or <c>null</c> if the screen saver
        /// can't be configured.
        /// </returns>
        Control CreateConfigurationControl();

        #endregion
    }
}
