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
using System.Threading;
#endregion

namespace ScreenSaver.Media
{
    /// <summary>
    /// Encapsulates the functionality of a simple image slideshow.
    /// </summary>
    public class SlideShow
    {
        #region Constants and Fields

        /// <summary>
        /// An implementation of the <see cref="ISlideShowConfiguration"/> interface.
        /// </summary>
        private ISlideShowConfiguration configuration;

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
        /// <exception cref="ArgumentNullException"><c>configuration</c> is null.</exception>
        public SlideShow(ISlideShowConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.configuration = configuration;
            this.slidesRenderingThread = null;
        }

        #endregion

        #region Events

        /// <summary>
        /// Triggered when a new image of the slideshow has been rendered.
        /// </summary>
        public event EventHandler ImageRendered;

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
            try
            {
                while (true)
                {
                    try
                    {

                    }
                    catch (ThreadAbortException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        // Ignore exceptions
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
