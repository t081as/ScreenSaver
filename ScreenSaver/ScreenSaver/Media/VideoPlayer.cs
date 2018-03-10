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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
#endregion

namespace ScreenSaver.Media
{
    /// <summary>
    /// Encapsulates the functionality of a video player.
    /// </summary>
    public class VideoPlayer
    {
        #region Constants and Fields

        /// <summary>
        /// Contains the unique identifier of this mci instance.
        /// </summary>
        private string identifier;

        /// <summary>
        /// References the control used for video playback.
        /// </summary>
        private Control parent;

        /// <summary>
        /// Indicates if a file has been opened.
        /// </summary>
        private bool isOpen;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoPlayer"/> class.
        /// </summary>
        /// <param name="identifier">The identifier of this instance.</param>
        /// <param name="parent">The <see cref="Control"/> that shall be used for video playback.</param>
        /// <exception cref="NotSupportedException">Current thread must be set to single thread apartment mode.</exception>
        /// <exception cref="ArgumentException">The identifier is invalid.</exception>
        /// <exception cref="ArgumentNullException"><c>parent</c> is null.</exception>
        public VideoPlayer(string identifier, Control parent)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new NotSupportedException("Current thread must be set to single thread apartment mode");
            }

            if (string.IsNullOrEmpty(identifier) || !Regex.IsMatch(identifier, "([A-Z])+"))
            {
                throw new ArgumentException("Identifier invalid");
            }

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            this.identifier = identifier;
            this.parent = parent;
            this.isOpen = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether a file has been opened.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens a video file for playback.
        /// </summary>
        /// <param name="fileName">The path and filename of the file.</param>
        /// <exception cref="InvalidOperationException">A file has already been opened.</exception>
        /// <exception cref="FileNotFoundException"><c>file</c> does not exist.</exception>
        public void Open(string fileName)
        {
            if (this.isOpen)
            {
                throw new InvalidOperationException("File has already been opened");
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File not found", fileName);
            }

            SendCommand(string.Format("OPEN \"{0}\" ALIAS {1} PARENT {2} STYLE CHILD", fileName, this.identifier, this.parent.Handle));
            this.isOpen = true;
        }

        /// <summary>
        /// Closes the video file.
        /// </summary>
        /// <exception cref="InvalidOperationException">File has not been opened.</exception>
        public void Close()
        {
            if (!this.isOpen)
            {
                throw new InvalidOperationException("File has not been opened");
            }

            SendCommand(string.Format("CLOSE {0}", this.identifier));
            this.isOpen = false;
        }

        /// <summary>
        /// Sends the given command to the multimedia interface.
        /// </summary>
        /// <param name="command">The command that shall be sent.</param>
        /// <exception cref="NotSupportedException">Current thread must be set to single thread apartment mode.</exception>
        /// <exception cref="ApplicationException">An error occured while sending the command.</exception>
        private static void SendCommand(string command)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new NotSupportedException("Current thread must be set to single thread apartment mode");
            }

            int ret = NativeMethods.MciSendString(command, null, 0, IntPtr.Zero);

            if (ret != 0)
            {
                StringBuilder errText = new StringBuilder(256);
                NativeMethods.MciGetErrorString(ret, errText, 256);

                throw new ApplicationException(string.Format("{0} ({1})", errText.ToString(), ret.ToString()));
            }
        }

        #endregion
    }
}
