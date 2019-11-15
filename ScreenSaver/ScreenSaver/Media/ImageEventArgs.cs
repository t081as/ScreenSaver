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
#endregion

namespace ScreenSaver.Media
{
    /// <summary>
    /// Provides data for the <see cref="SlideShow.ImageRendered"/> event.
    /// </summary>
    public class ImageEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageEventArgs"/> class.
        /// </summary>
        public ImageEventArgs()
        {
            this.Image = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageEventArgs"/> class.
        /// </summary>
        /// <param name="image">The image that has been rendered.</param>
        public ImageEventArgs(Image image)
        {
            this.Image = image;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image that has been rendered.
        /// </summary>
        public Image Image
        {
            get;
            set;
        }

        #endregion
    }
}
