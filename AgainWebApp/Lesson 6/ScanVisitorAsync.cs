using ConsoleApp1.HomeWork.Lesson_6;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5
{
    internal class ScanVisitorAsync:IVisitAsync
    {
        public string Datas { get; private set; }
        public async Task VisitAsync(DirectoryInfo info)
        {
            foreach (var file in info.GetFiles())
            {
                //Datas = file.ReadAllText();
            }
            foreach(var item in info.EnumerateDirectories())
            {
               await VisitAsync(item);
            }
        }
        public void Clears() => Datas = null;
        public IEnumerable<string> ReadLine(FileInfo file)
        {
            using var reader=file.OpenText();
            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine();
            }
        }

      
    }
}
