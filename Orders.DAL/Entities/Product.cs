using Orders.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DAL.Entities
{
    public class Product:NamedEntity
    {
        [Column(TypeName ="decimal(18,2)")]//ттут мы указываем какой тип для типа decimal будет соотвествовать в базе данных 
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
