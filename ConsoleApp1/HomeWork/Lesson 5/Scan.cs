using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5
{
    internal class Scan
    {
        public void ScanFile(string NameFile)
        {
            var visitors = new ScanVisitorAsync();
            var dir= new DirectoryInfo(NameFile);
            visitors.VisitAsync(dir);
        }
    }
}
