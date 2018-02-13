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
        }

        #endregion
    }
}
