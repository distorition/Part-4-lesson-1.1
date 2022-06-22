using RazorEngine;
using RazorEngine.Templating;
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
        private const string TemplateTex = @"Каталог товаров
Название:@Model.CatalogName

Колличество товаров: @Model.Products.Count()
Время Формирвоания:@Model.CatalogCreationTime.ToString(""dd.MM.yyyy HH:mm:ss"")
Описание:@Model.CatalogDiscription

id   name   Description     Price
@foreach(var product in Model.Products)
{
@product.id     @product.name   @product.Description    @product.Price.ToString(""c"")
}

@*Полная Стоимость @Model.Products.Sum(p=>p.Price).ToString(""c"")*@
               
Полная Стоимость @Model.TotalPrice.ToString(""c"")
";// так мы при помощи Razora создаем текстовый документ 

        public string CatalogName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public IEnumerable<(int id, string name, string Description, decimal Price)> Products { get; set ; }

        public FileInfo Create(string ReportTeamplane)
        {
           var report_file=  new FileInfo(ReportTeamplane);
            report_file.Delete();

            var tamplete_text = TemplateTex;
            var result = Engine.Razor.RunCompile(tamplete_text,"ProductsTemplate",null,new //данный метод генерирует нам результат в текстовом виде 
            {
                //эти параметры мы будем извлекать для полей в документе при помщи Razor

                CatalogName,
               CatalogCreationTime= DateTime,
               CatalogDiscription= Description,
                Products=Products
                .Select(p=>new
                {
                    p.name,
                    p.Description,
                    p.Price,
                    p.id
                })
                .ToArray(),//добавляем каталог с нашими товарами 
                TotalPrice=Products.Sum(p=>p.Price)
            });//текст шаблона , потом название шаблона , а потом модель 

            using (var write = report_file.CreateText())// а потом мы записываем результат 
            {
                write.Write(result);
            }

            report_file.Refresh();
            return report_file;
        }
    }
}
