using ReportsExamples.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsExamples.Reports
{
    internal class ReportRazor : IProductReports
    {
        public string CatalogName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public IEnumerable<(int id, string name, string Description, decimal Price)> Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FileInfo Create(string ReportTeamplane)
        {
           var report_file=  new FileInfo(ReportTeamplane);
            report_file.Delete();



            report_file.Refresh();
            return report_file;
        }
    }
}
