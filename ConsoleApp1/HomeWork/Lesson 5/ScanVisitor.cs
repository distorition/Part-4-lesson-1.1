using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_5
{
    internal class ScanVisitor
    {
        public string Data { get; private set; }
        public void Visit(DirectoryInfo info)
        {
            foreach (var file in info.GetFiles())
            {
                Data = file.ReadAllText();
            }
            foreach(var item in info.EnumerateDirectories())
            {
                Visit(item);
            }
        }
        public void Clear() => Data = null;
        public IEnumerable<string> ReadLines(FileInfo file)
        {
            using var reader=file.OpenText();
            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine();
            }
        }
    }
}
