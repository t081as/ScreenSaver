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
using System.IO;
using System.Windows.Forms;
using ScreenSaver.Media;
#endregion

namespace ScreenSaver.Test
{
    /// <summary>
    /// A screen saver control testing the <see cref="SlideShow"/> class.
    /// </summary>
    public partial class SlideShowControl : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The slide show provider.
        /// </summary>
        private SlideShow slideShow;

        /// <summary>
        /// The image that shall be displayed.
        /// </summary>
        private Image currentImage;

        /// <summary>
        /// The lock object for <see cref="currentImage"/>.
        /// </summary>
        private object currentImageLock = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideShowControl"/> class.
        /// </summary>
        public SlideShowControl()
        {
            this.InitializeComponent();

            this.slideShow = new SlideShow(new SlideShowTestConfiguration());
            this.slideShow.ImageRendered += this.SlideShow_ImageRendered;
            this.slideShow.Error += this.SlideShow_Error;

            this.slideShow.Start();
        }

        #endregion

        #region Methods

        /// <summary>
        /// A rendering error occured.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">A <see cref="RenderingErrorEventArgs"/> containing error information.</param>
        private void SlideShow_Error(object sender, RenderingErrorEventArgs e)
        {
            File.AppendAllText("error.txt", e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            Environment.Exit(1);
        }

        /// <summary>
        /// A slide show image has been rendered.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">An <see cref="ImageEventArgs"/> containing the rendered image.</param>
        private void SlideShow_ImageRendered(object sender, ImageEventArgs e)
        {
            lock (this.currentImageLock)
            {
                this.currentImage = e.Image;
            }

            this.Invalidate();
        }

        /// <summary>
        /// Handles the paint event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">A <see cref="PaintEventArgs"/> containing the reference to the <see cref="Graphics"/> object of the window.</param>
        private void SlideShowControl_Paint(object sender, PaintEventArgs e)
        {
            lock (this.currentImageLock)
            {
                if (this.currentImage != null)
                {
                    e.Graphics.DrawImage(this.currentImage, 0, 0, this.Width, this.Height);
                }
            }
        }

        #endregion
    }
}
