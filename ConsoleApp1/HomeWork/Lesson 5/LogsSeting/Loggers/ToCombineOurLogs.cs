using ConsoleApp1.HomeWork.Lesson_5.InterfaceLoger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5.Loggers
{
    internal class ToCombineOurLogs : ForLogs
    {
        private ILoger[] loger;
        public IReadOnlyCollection<ILoger> Loger => loger;
        public ToCombineOurLogs(params ILoger[] logers)
        {
            loger = logers;
        }
        public override void Log(string Message)
        {
            foreach (var log in loger)
            {
                log.Log(Message);
            }
        }
    }
}
