using Orders.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DAL.Entities
{
    public class Buyer:NamedEntity
    {
        public string? LastName { get; set; } 
        public string? Patronomic { get; set; }
        public DateTime Birthday { get; set; }
    }
}
