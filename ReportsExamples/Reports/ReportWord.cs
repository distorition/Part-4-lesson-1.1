using ReportsExamples.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;

namespace ReportsExamples.Reports
{
    public class ReportWord : IProductReports
    {
        private const string _CreationTime = "CreationTime";
        private const string FiledProduct = "FieldProduct";
        private const string _Description = "Description";
        private const string _CatalogName = "CatalogName";
        private const string _CategoryId = "CategoryID";

        private readonly FileInfo _TemplateFile;
        public string CatalogName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public IEnumerable<(int id, string name, string Description, decimal Price)> Products { get; set ; }

        public ReportWord(string TemPlateFilePath)
        {
            _TemplateFile= new FileInfo(TemPlateFilePath);

        }

        public FileInfo Create(string ReportTeamplane)// генератор отчета 
        {
            if (!_TemplateFile.Exists)
            {
                throw new FileNotFoundException("где файл ?", _TemplateFile.FullName);
            }

            var reportFile=new FileInfo(ReportTeamplane);//копируем фаил
            if (reportFile.Exists)
            {
                reportFile.Delete();
            }
            _TemplateFile.CopyTo(reportFile.FullName);

            var rows = Products.Select(p => new TableRowContent(new List<FieldContent>//строки таблицы для нашего ворлд файла ( я его не делал)
            {
                new FieldContent(_CategoryId,p.id.ToString()),
                new FieldContent(_Description,p.Description),
                new FieldContent(_CatalogName,p.name),
                
            })).ToArray();

            var content = new Content(//так мы заполняем поля в нашем вордовском документе( я егго не делал)
                new FieldContent(_CreationTime, DateTime.ToString()),
                new FieldContent(_CatalogName, CatalogName),
                TableContent.Create(FiledProduct,rows));

            using var doc = new TemplateProcessor(reportFile.FullName).SetRemoveContentControls(true);// тут указываем файл с которым будет работать и удаляем пустые поля после замены значений 

            doc.FillContent(content);//заполняем документ содержимым
            doc.SaveChanges();

            reportFile.Refresh();//обвноялем его состояния  

            return reportFile;

        }
    }
}
