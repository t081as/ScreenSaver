using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaver.Test
{
    /// <summary>
    /// Contains main entry point of the application.
    /// </summary>
    public static class Program
    {
        #region Methods

        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        #endregion
    }
}