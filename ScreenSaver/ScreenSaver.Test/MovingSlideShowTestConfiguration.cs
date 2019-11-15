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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenSaver.Media;
#endregion

namespace ScreenSaver.Test
{
    /// <summary>
    /// Test configuration for the <see cref="MovingSlideShow"/> class.
    /// </summary>
    internal class MovingSlideShowTestConfiguration : ISlideShowConfiguration
    {
        #region Constants and Fields

        /// <summary>
        /// The predefined items of this slide show.
        /// </summary>
        private List<SlideShowItem> slideShowItems;

        /// <summary>
        /// Stores the current position in the list of slide show items.
        /// </summary>
        private int currentIndex;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MovingSlideShowTestConfiguration"/> class.
        /// </summary>
        public MovingSlideShowTestConfiguration()
        {
            this.slideShowItems = new List<SlideShowItem>();
            this.currentIndex = 0;

            int slideDisplayTime = 11000;

            string localUserImageDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string localUserTestImageDirectory = Path.Combine(localUserImageDirectory, "Test");
            string imagePath;

            if (Directory.Exists(localUserTestImageDirectory))
            {
                imagePath = localUserTestImageDirectory;
            }
            else
            {
                imagePath = localUserImageDirectory;
            }

            foreach (string imageFile in Directory.GetFiles(imagePath, "*.jpg"))
            {
                this.slideShowItems.Add(new SlideShowItem(Image.FromFile(imageFile), slideDisplayTime));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the next element of the slide show.
        /// </summary>
        /// <returns>The next element of the slide show.</returns>
        public SlideShowItem Next()
        {
            SlideShowItem currentItem;

            if (this.currentIndex >= this.slideShowItems.Count)
            {
                this.currentIndex = 0;
            }

            currentItem = this.slideShowItems[this.currentIndex];
            this.currentIndex++;

            return currentItem;
        }

        #endregion
    }
}
