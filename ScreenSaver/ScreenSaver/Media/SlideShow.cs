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
using System.Drawing.Imaging;
using System.Threading;
#endregion

namespace ScreenSaver.Media
{
    /// <summary>
    /// Encapsulates the functionality of a simple image slideshow.
    /// </summary>
    public class SlideShow : ISlideShow
    {
        #region Constants and Fields

        /// <summary>
        /// An implementation of the <see cref="ISlideShowConfiguration"/> interface.
        /// </summary>
        private ISlideShowConfiguration configuration;

        /// <summary>
        /// The desired <see cref="Size"/> of the rendered images.
        /// </summary>
        private Size renderedImageSize;

        /// <summary>
        /// Reference to the thread rendering the slideshow.
        /// </summary>
        private Thread slidesRenderingThread;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideShow"/> class.
        /// </summary>
        /// <param name="configuration">An implementation of the <see cref="ISlideShowConfiguration"/> interface.</param>
        /// <param name="renderedImageSize">The desired <see cref="Size"/> of the rendered images.</param>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is null.</exception>
        public SlideShow(ISlideShowConfiguration configuration, Size renderedImageSize)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.renderedImageSize = renderedImageSize;
            this.configuration = configuration;
            this.slidesRenderingThread = null;
        }

        #endregion

        #region Events

        /// <summary>
        /// Triggered when a new image of the slideshow has been rendered.
        /// </summary>
        public event EventHandler<ImageEventArgs> ImageRendered;

        /// <summary>
        /// Triggered when the rendering fails.
        /// </summary>
        public event EventHandler<RenderingErrorEventArgs> Error;

        #endregion

        #region Methods

        /// <summary>
        /// Starts the Slideshow.
        /// </summary>
        /// <exception cref="InvalidOperationException">The slideshow has already been started.</exception>
        public void Start()
        {
            if (this.slidesRenderingThread != null)
            {
                throw new InvalidOperationException("Slideshow has already been started");
            }

            this.slidesRenderingThread = new Thread(new ThreadStart(this.SlideRendering));
            this.slidesRenderingThread.IsBackground = true;
            this.slidesRenderingThread.Name = "Slideshow Rendering";

            this.slidesRenderingThread.Start();
        }

        /// <summary>
        /// Stops the slideshow.
        /// </summary>
        /// <exception cref="InvalidOperationException">The slideshow has not been started.</exception>
        public void Stop()
        {
            if (this.slidesRenderingThread == null)
            {
                throw new InvalidOperationException("Slideshow has not been started");
            }

            this.slidesRenderingThread.Abort();
            this.slidesRenderingThread = null;
        }

        /// <summary>
        /// Rendering method executed in a background thread.
        /// </summary>
        protected virtual void SlideRendering()
        {
            const int slideChangeTime = 1000; // The time in milliseconds used for the fade in of the new image
            const int slideChangeSteps = 50; // The number of fade in steps performed

            Bitmap renderingImage = new Bitmap(this.renderedImageSize.Width, this.renderedImageSize.Height);

            using (Graphics graphics = Graphics.FromImage(renderingImage))
            {
                graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, renderingImage.Width, renderingImage.Height);
            }

            try
            {
                while (true)
                {
                    try
                    {
                        // Retrieve next slide show item
                        SlideShowItem nextItem = this.configuration.Next();
                        int realDisplayTime = nextItem.DisplayTime;

                        // Check next slide show item
                        if (nextItem.DisplayImage == null)
                        {
                            throw new InvalidOperationException("ISlideShowConfiguration.Next: SlideShowItem.DisplayImage is null");
                        }

                        if (realDisplayTime < 1000)
                        {
                            realDisplayTime = 1000;
                        }

                        for (int i = 0; i < slideChangeSteps; i++)
                        {
                            using (Graphics graphics = Graphics.FromImage(renderingImage))
                            {
                                ColorMatrix matrix = new ColorMatrix();
                                matrix.Matrix33 = 1f / (float)(slideChangeSteps - i); // Opacity

                                ImageAttributes imageAttributes = new ImageAttributes();
                                imageAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                graphics.DrawImage(
                                    nextItem.DisplayImage,
                                    new Rectangle(0, 0, renderingImage.Width, renderingImage.Height),
                                    0,
                                    0,
                                    nextItem.DisplayImage.Width,
                                    nextItem.DisplayImage.Height,
                                    GraphicsUnit.Pixel,
                                    imageAttributes);
                            }

                            Thread.Sleep((int)(slideChangeTime / slideChangeSteps));
                            this.ImageRendered?.Invoke(this, new ImageEventArgs(renderingImage));
                        }

                        GC.Collect();
                        Thread.Sleep(realDisplayTime);
                    }
                    catch (ThreadAbortException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        this.Error?.Invoke(this, new RenderingErrorEventArgs(ex));
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }

        #endregion
    }
}
