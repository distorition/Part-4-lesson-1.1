using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5.Loggers
{
    internal class ConsoleLogs : ForLogs
    {
        private static readonly object logLock = new object();
        private static ConsoleLogs? consoole;
        public static ConsoleLogs ConsoleLog { get; } = new ConsoleLogs();
        public static ConsoleLogs Logs=> consoole?? new ConsoleLogs();
        /// <summary>
        ///шаблон одиночка с блокировкой 
        /// </summary>
        public static ConsoleLogs Consoole { 
            get {
                if (consoole != null)
                {
                    return consoole;
                }
                lock (logLock)
                {
                    if (consoole != null)
                    {
                        return consoole;
                    }
                    consoole = new ConsoleLogs();
                    return consoole;
                }
            } }
        public override void Log(string Message)
        {
            Console.WriteLine($"{DateTime.Now:yyy.M.ddTHH:mm:ss.fff}-{Message}");
        }
    }
}
