using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BehaviorPatterns.Visitor
{
    internal class VisitorExample
    {
        public static void Run()
        {
            const string fileName = @"..\..\..\..";
            var visitos= new SourceCounterVisiter();
            var dir=new DirectoryInfo(fileName);
            visitos.Visit(dir);
        }
    }

    public class SourceCounterVisiter
    {
        public int Count { get; private set; }
        public void Visit(DirectoryInfo directoru)
        {
            foreach (var file in directoru.EnumerateFiles("*.cs"))
            {
                var count= GetLineCount(file);
                Count += count;
            }
            foreach (var item in directoru.EnumerateDirectories()) //мы перебираем наши директории и при помщи метода Visit посещаем их
            {
                Visit(item);
            }
        }
        public void Clear()=>Count=0;
        private int GetLineCount(FileInfo file)
        {
            var count = GetLines(file).Select(s=>s.Trim()).Count(str=>str.Length>0&&!str.StartsWith("//"));//то есть берем только те  строки чья длинна больше 0 и не начинаются с //
            return count;
        }
        private IEnumerable<string> GetLines(FileInfo file)
        {
            using var reader = file.OpenText();
            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine();
            }
        }
    }
}
