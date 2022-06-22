
using ReportsExamples.Infstructure;
using ReportsExamples.Interfaces;
using ReportsExamples.Models;
using ReportsExamples.Reports;

var rndm= new Random(Environment.TickCount);// для отслеживания начального состояния ( либо задать просто число и тогда у нас всегда будет одна последовательность что удобна для тестирования)
const int ProductCount = 20;
var product = Enumerable.Range(1, ProductCount)
    .Select(x => new Product(x, $"Product{x}", $"Description-{x}", (decimal)(Random.Shared.NextDouble() * 1000)))//св-ва Shared предоставляет доступ к одному единственному обьекту Rndom (то есть в нутри каждого потока будет свой обьект рандом ) чтобы у нас не получилось везде одно чиссло 
    .ToArray();

//Convert.ChangeType("123", typeof(int));// что конвертируем ("123") , вво что конвертируем ( в int)

var catalog = new ProductsCatalog("Каталог товаров", "Описание товаров", DateTime.Now, product);


const string TeamPlateFile = "temlate.docx";
IProductReports productReports = new ReportWord(TeamPlateFile);
const string ReportFileNmae = "report.docx";
CreateReport( productReports, catalog, ReportFileNmae);

static void CreateReport(IProductReports generator, ProductsCatalog catalog,string TeamplateFile)
{
    generator.CatalogName = catalog.name;
    generator.Description = catalog.Description;
    generator.DateTime = catalog.DateTime;

    generator.Products = catalog.Products.Select(product => (product.id, product.name, product.Description, product.Price));//таким образом мы передаем сразу 4 параметра 

    var report_file=generator.Create(TeamplateFile);
    report_file.Execute();
}

Console.WriteLine("Hello, World!");
