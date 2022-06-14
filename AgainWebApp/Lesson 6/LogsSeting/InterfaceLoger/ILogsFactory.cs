using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5.InterfaceLoger
{
    internal interface ILogsFactory
    {
        ILoger Create();
    }
}
