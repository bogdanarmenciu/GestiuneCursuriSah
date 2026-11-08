using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestiunePrezente.Common;

namespace GestiunePrezente.MeniuPrincipal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmMeniuPrincipal frm = new(args);

            if (frm.GetManagerDeProiect() != null)
                Application.Run(frm);
        }
    }
}
