using ReportsExamples.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsExamples.Reports
{
    public class ReportWord : IProductReports
    {
        public string CatalogName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }

        public FileInfo Create(string ReportTeamplane)
        {
            throw new NotImplementedException();
        }
    }
}
