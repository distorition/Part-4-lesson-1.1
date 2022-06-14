using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_6
{
    internal interface IVisitAsync
    {
        Task VisitAsync(DirectoryInfo info);
    }
}
