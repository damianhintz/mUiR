using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace muir
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Process.Start("cmd.exe", "/c start e:\test");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
