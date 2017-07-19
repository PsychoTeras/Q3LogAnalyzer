using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Q3LogAnalyzer.Forms;

namespace Q3LogAnalyzer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<KeyValuePair<string, string>> procs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("proc1", "cpt1"),
                new KeyValuePair<string, string>("proc2", "cpt2")
            };

            List<KeyValuePair<string, string>> accs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("acc2", "cpt2"),
                new KeyValuePair<string, string>("acc1", "cpt1"),
                new KeyValuePair<string, string>("acc3", "cpt3")
            };

            var x = accs.OrderBy(c => procs.IndexOf(procs.FirstOrDefault(p => p.Value == c.Value))).ToArray();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
