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
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace ScreenSaver
{
    /// <summary>
    /// The <see cref="Form"/> hosting the screen saver control.
    /// </summary>
    internal partial class ScreenSaverForm : Form
    {
        #region Constants and Fields

        /// <summary>
        /// Stores the initial mouse cursor location.
        /// </summary>
        private Point originalLocation = new Point(int.MaxValue, int.MaxValue);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSaverForm"/> class.
        /// </summary>
        /// <param name="screenSaverControl">The control that shall be used as screen saver.</param>
        /// <param name="screen">The screen that shall host this screen saver form instance.</param>
        public ScreenSaverForm(Control screenSaverControl, Screen screen)
        {
            this.InitializeComponent();

            this.Bounds = screen.Bounds;
            this.Controls.Add(screenSaverControl);
            screenSaverControl.Dock = DockStyle.Fill;

            screenSaverControl.MouseMove += this.ScreenSaverForm_MouseMove;
            screenSaverControl.MouseClick += this.ScreenSaverForm_MouseClick;
            screenSaverControl.KeyPress += this.ScreenSaverForm_KeyPress;

            Cursor.Hide();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the KeyPress event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">A <see cref="KeyPressEventArgs"/>.</param>
        private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the MouseClick event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">A <see cref="MouseEventArgs"/>.</param>
        private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the MouseMove event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">A <see cref="MouseEventArgs"/>.</param>
        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.originalLocation.X == int.MaxValue && this.originalLocation.Y == int.MaxValue)
            {
                this.originalLocation = e.Location;
            }

            if (Math.Abs(e.X - this.originalLocation.X) > 40 || Math.Abs(e.Y - this.originalLocation.Y) > 40)
            {
                Application.Exit();
            }
        }

        #endregion
    }
}