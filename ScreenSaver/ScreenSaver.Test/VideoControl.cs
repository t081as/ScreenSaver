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
    /// A screen saver control testing the <see cref="VideoPlayer"/> class.
    /// </summary>
    public partial class VideoControl : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// Reference to the video player class.
        /// </summary>
        private VideoPlayer player;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoControl"/> class.
        /// </summary>
        public VideoControl()
        {
            this.InitializeComponent();
            this.CreateControl();
            this.player = new VideoPlayer("SCR", this);

            string videoFileName = Environment.GetEnvironmentVariable("TESTVIDEO");

            if (string.IsNullOrEmpty(videoFileName))
            {
                this.labelErrorMessage.Dock = DockStyle.Fill;
                this.labelErrorMessage.Visible = true;
                this.labelErrorMessage.Text = "Please set environment variable %TESTVIDEO%: path and filename of video file";
            }
            else
            {
                if (!File.Exists(videoFileName))
                {
                    this.labelErrorMessage.Dock = DockStyle.Fill;
                    this.labelErrorMessage.Visible = true;
                    this.labelErrorMessage.Text = $"PEnvironment variable %TESTVIDEO%: file {videoFileName} not found";
                }
                else
                {
                    this.player.Open(videoFileName);
                    this.player.Resize();
                    this.player.Mute();
                    this.player.PlayLoop();
                }
            }
        }

        #endregion
    }
}
