using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsExamples.Models
{
    public record Product(int id, string name,string Description,decimal Price);//record это класс который сам делает за нас некоторые функции


}
