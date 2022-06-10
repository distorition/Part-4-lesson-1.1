using Orders.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DAL.Entities
{
    internal class Order:Entity
    {
        public DateTime Date { get; set; }
        [Required]
        public string Adress { get; set; } = null!;
        [Required]
        public string Phonee { get; set; }=null!;
        [Required]
        public Buyer buyer { get; set; } = null!;
        public ICollection<Orderitem> Items { get; set; }=new HashSet<Orderitem>();//и этого свойтсва  то есть ( к каждому заказу может принадлежать множесто элементов заказа )
    }
}
