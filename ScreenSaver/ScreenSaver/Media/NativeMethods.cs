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
using System.Runtime.InteropServices;
using System.Text;
#endregion

namespace ScreenSaver.Media
{
    /// <summary>
    /// Encapsulates native methods.
    /// </summary>
    internal class NativeMethods
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeMethods"/> class.
        /// </summary>
        private NativeMethods()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The mciSendString function sends a command string to an MCI device.
        /// </summary>
        /// <param name="cmd">The MCI command string.</param>
        /// <param name="ret">A buffer that receives return information.</param>
        /// <param name="retLen">The size of the return buffer.</param>
        /// <param name="hwnd">A handle to a callback window.</param>
        /// <returns>Returns zero if successful or an error otherwise.</returns>
        [DllImport("winmm.dll", CharSet = CharSet.Auto, EntryPoint = "mciSendString")]
        public static extern int MciSendString(string cmd, StringBuilder ret, int retLen, IntPtr hwnd);

        /// <summary>
        /// Retrieves a string that describes the specified MCI error code.
        /// </summary>
        /// <param name="errCode">The error return code.</param>
        /// <param name="errText">A buffer for the text describing the error.</param>
        /// <param name="errLen">The length of the buffer.</param>
        /// <returns>Returns TRUE if successful or FALSE if the error code is not known.</returns>
        [DllImport("winmm.dll", CharSet = CharSet.Auto, EntryPoint = "mciGetErrorString")]
        public static extern int MciGetErrorString(int errCode, StringBuilder errText, int errLen);

        #endregion
    }
}
