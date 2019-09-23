using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFErrorHandling
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TextWriterTraceListener myTextListener = new TextWriterTraceListener("othertrace.txt");
            Trace.Listeners.Add(myTextListener);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new Form1());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.TraceError("There's been an error captain: {0}", e);
            Trace.Flush();
            Debug.WriteLine(e.ExceptionObject);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Trace.TraceError($@"
            ------ Exception Thrown at {DateTime.Now} -------
            Message: {e.Exception.Message}
            Stacktrace:
            {e.Exception.StackTrace}
            ");

            Trace.Flush();
        }
    }
}
