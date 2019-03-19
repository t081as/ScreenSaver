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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
#endregion

namespace ScreenSaver.Media
{
    /// <summary>
    /// Encapsulates the functionality of a moving image slideshow.
    /// </summary>
    public class MovingSlideShow : ISlideShow
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
        /// Initializes a new instance of the <see cref="MovingSlideShow"/> class.
        /// </summary>
        /// <param name="configuration">An implementation of the <see cref="ISlideShowConfiguration"/> interface.</param>
        /// <param name="renderedImageSize">The desired <see cref="Size"/> of the rendered images.</param>
        /// <exception cref="ArgumentNullException"><c>configuration</c> is null.</exception>
        public MovingSlideShow(ISlideShowConfiguration configuration, Size renderedImageSize)
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
            const int minDisplayTime = 3000;
            const int maxDisplayTime = 13000;
            const int renderThreadSleepTime = 30; // Sleep time of the rendering thread in milliseconds

            Random rnd = new Random();
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

                        if (nextItem.DisplayImage.Width < 1000 || nextItem.DisplayImage.Height < 1000)
                        {
                            throw new InvalidOperationException("ISlideShowConfiguration.Next: SlideShowItem.DisplayImage: minimal image size is 1000x1000 px");
                        }

                        if (realDisplayTime < minDisplayTime)
                        {
                            realDisplayTime = minDisplayTime;
                        }

                        if (realDisplayTime > maxDisplayTime)
                        {
                            realDisplayTime = maxDisplayTime;
                        }

                        int zoom = 0;
                        while (zoom == 0)
                        {
                            zoom = rnd.Next(-1, 2);
                        }

                        Rectangle renderingRectangle = default(Rectangle);
                        double ratio = (double)nextItem.DisplayImage.Width / (double)nextItem.DisplayImage.Height;

                        if (zoom > 0)
                        {
                            // zoom in
                            renderingRectangle.Width = rnd.Next((int)(nextItem.DisplayImage.Width * 0.75) + 50, nextItem.DisplayImage.Width + 1);
                            renderingRectangle.Height = (int)Math.Round(renderingRectangle.Width / ratio, 0);
                            renderingRectangle.X = rnd.Next(0, nextItem.DisplayImage.Width - renderingRectangle.Width + 1);
                            renderingRectangle.Y = rnd.Next(0, nextItem.DisplayImage.Height - renderingRectangle.Height + 1);
                        }
                        else if (zoom < 0)
                        {
                            // zoom out
                            renderingRectangle.Height = rnd.Next((int)(nextItem.DisplayImage.Height * 0.5) + 50, nextItem.DisplayImage.Height - 300 + 1);
                            renderingRectangle.Width = (int)Math.Round(renderingRectangle.Height * ratio, 0);
                            renderingRectangle.X = rnd.Next(150, nextItem.DisplayImage.Width - renderingRectangle.Width - 150 + 1);
                            renderingRectangle.Y = rnd.Next(150, nextItem.DisplayImage.Height - renderingRectangle.Height - 150 + 1);
                        }

                        long i = 0;
                        Stopwatch watch = new Stopwatch();
                        watch.Start();

                        while (true)
                        {
                            renderingRectangle.X = renderingRectangle.X + zoom;
                            renderingRectangle.Y = renderingRectangle.Y + zoom;
                            renderingRectangle.Width = renderingRectangle.Width - (2 * zoom);
                            renderingRectangle.Height = renderingRectangle.Height - (2 * zoom);

                            using (Graphics graphics = Graphics.FromImage(renderingImage))
                            {
                                // Security fallback if rendering is too fast
                                if (i < 150)
                                {
                                    // Fade in: 10 steps
                                    if (i < 10)
                                    {
                                        ColorMatrix matrix = new ColorMatrix();
                                        matrix.Matrix33 = 1f / (float)(10 - i); // Opacity

                                        ImageAttributes imageAttributes = new ImageAttributes();
                                        imageAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        graphics.DrawImage(
                                            nextItem.DisplayImage,
                                            new Rectangle(0, 0, renderingImage.Width, renderingImage.Height),
                                            renderingRectangle.Left,
                                            renderingRectangle.Top,
                                            renderingRectangle.Width,
                                            renderingRectangle.Height,
                                            GraphicsUnit.Pixel,
                                            imageAttributes);
                                    }
                                    else
                                    {
                                        graphics.DrawImage(
                                            nextItem.DisplayImage,
                                            new Rectangle(0, 0, renderingImage.Width, renderingImage.Height),
                                            renderingRectangle,
                                            GraphicsUnit.Pixel);
                                    }
                                }
                            }

                            this.ImageRendered?.Invoke(this, new ImageEventArgs(renderingImage));
                            i++;

                            Thread.Sleep(renderThreadSleepTime);

                            if (watch.ElapsedMilliseconds > realDisplayTime)
                            {
                                break;
                            }
                        }

                        Debug.WriteLine("END-END-END");

                        watch.Stop();
                        GC.Collect();
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
