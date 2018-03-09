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
using System.Text;
using System.Threading;
#endregion

namespace ScreenSaver.Media
{
    /// <summary>
    /// Encapsulates the functionality of a video player.
    /// </summary>
    public class VideoPlayer
    {
        #region Constants and Fields

        #endregion

        #region Constructors and Destructors

        #endregion

        #region Methods

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
