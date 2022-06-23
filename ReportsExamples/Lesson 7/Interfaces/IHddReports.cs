using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsExamples.Lesson_7.Interfaces
{
    internal interface IHddReports
    {
        string HddName { get;set;}
        int CountHddSpace { get; set; } 
        
        IEnumerable<(int Field, int empty)> Count { get;set; }

        FileInfo Create(string hddName);
    }
}
