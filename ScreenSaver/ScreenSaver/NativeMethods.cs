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
using System.Runtime.InteropServices;
#endregion

namespace ScreenSaver
{
    /// <summary>
    /// Encapsulates native methods.
    /// </summary>
    internal class NativeMethods
    {
        #region Constants and Fields

        /// <summary>
        /// Sets a new window style.
        /// </summary>
        public const int GWL_STYLE = -16;

        /// <summary>
        /// The window is a child window.
        /// </summary>
        public const int WS_CHILD = 0x40000000;

        #endregion

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
        /// Changes the parent window of the specified child window.
        /// </summary>
        /// <param name="hWndChild">A handle to the child window.</param>
        /// <param name="hWndNewParent">A handle to the new parent window.</param>
        /// <returns>If the function succeeds, the return value is a handle to the previous parent window.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// Changes an attribute of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nIndex">A zero-based offset to the value to be set.</param>
        /// <param name="dwNewLong">A replacement value. </param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified integer. If the function fails, the return value is zero.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Retrieves information about the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nIndex">A zero-based offset to the value to be get.</param>
        /// <returns>If the function succeeds, the return value is the requested value.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Retrieves the coordinates of a window's client area.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpRect">A pointer to a <see cref="Rectangle"/> structure that receives the client coordinates.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion
    }
}
