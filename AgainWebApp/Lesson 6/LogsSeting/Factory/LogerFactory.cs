using ConsoleApp1.HomeWork.Lesson_5.InterfaceLoger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5.Loggers
{
    internal abstract class LogerFactory : ILogsFactory
    {
        public abstract ILoger Create();
    }
}
