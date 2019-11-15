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
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace ScreenSaver.Test
{
    /// <summary>
    /// A test control used by the screen saver.
    /// </summary>
    public partial class ColorControl : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// A random number generator.
        /// </summary>
        private Random rnd = new Random();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorControl"/> class.
        /// </summary>
        public ColorControl()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the <see cref="Timer.Tick"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">An empty <see cref="EventArgs"/>.</param>
        protected virtual void TimerColor_Tick(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(this.rnd.Next(0, 256), this.rnd.Next(0, 256), this.rnd.Next(0, 256));
        }

        #endregion
    }
}
