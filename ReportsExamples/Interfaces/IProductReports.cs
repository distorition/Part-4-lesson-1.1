using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsExamples.Interfaces
{
    public interface IProductReports
    {
        string CatalogName { get; set; }
        DateTime DateTime { get; set; }
        string Description { get; set; }

        IEnumerable<(int id, string name,string Description,decimal Price)> Products { get; set; }

        FileInfo Create(string ReportTeamplane);
    }
}
