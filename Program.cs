using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integrated_Threat_Hunting_Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception)
            {
                MessageBox.Show("Administrative privilage is required to access 'Security' event logs\n\nPlease restart the application by right-clicking and selecting 'Run as Administrator'.", "Administrative privilages", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();              
            }
        }
    }
}
