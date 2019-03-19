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
    /// Encapsulates a single slide show image.
    /// </summary>
    public class SlideShowItem
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideShowItem"/> class.
        /// </summary>
        public SlideShowItem()
        {
            this.DisplayTime = 1000;
            this.DisplayImage = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideShowItem"/> class.
        /// </summary>
        /// <param name="displayImage">The image that shall be displayed.</param>
        /// <param name="displayTime">The time in milliseconds the image shall displayed.</param>
        public SlideShowItem(Image displayImage, int displayTime)
        {
            this.DisplayTime = displayTime;
            this.DisplayImage = displayImage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets time in milliseconds the image shall displayed.
        /// </summary>
        public int DisplayTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image that shall be displayed.
        /// </summary>
        public Image DisplayImage
        {
            get;
            set;
        }

        #endregion
    }
}
