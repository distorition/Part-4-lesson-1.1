using RazorEngine;
using RazorEngine.Templating;
using ReportsExamples.Lesson_7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsExamples.Lesson_7
{
    internal class RazzorReportFile : IHddReports
    {
        private const string ReportFileName = @"Данные дисков
Название:@Model.HddName
всего место на ддиске:@Model.Space
Cвободное  Место на диске:@Model.EmptySpace  Занятое место :@Model.FiledSpace

";
        public string HddName { get; set; }
        public int CountHddSpace { get; set; }
     
        public IEnumerable<(int Field, int empty)> Count { get; set; }

        private readonly FileInfo _file;
        public RazzorReportFile(string PathFile)
        {
            _file = new FileInfo(PathFile);
        }


        public FileInfo Create(string hddName)
        {
           var reportFile= new FileInfo(hddName);
            reportFile.Delete();
            string readText;
            if (_file != null)
            {
                if (!_file.Exists)
                {
                    throw new FileNotFoundException(_file.FullName);
                }
                using var reader=_file.OpenText();
                readText=reader.ReadToEnd();
            }
            else
            {
                readText = ReportFileName;
            }

            var result = Engine.Razor.RunCompile(readText, "RazzorTemplate", null, new
            {
              name=  HddName,
              Space= CountHddSpace,
           
              FiledSpace=Count.Sum(p=>p.Field),
              EmptySpace=Count.Sum(p=>p.empty),
            });

            using( var text= reportFile.CreateText())
            {
                text.Write(result);
            }

            reportFile.Refresh();
            return reportFile;
        }
    }
}
