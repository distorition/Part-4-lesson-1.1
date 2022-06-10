using Orders.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DAL.Entities
{
    internal class Orderitem:Entity
    {
        public Product product { get; set; } = null!;// таким образом каждому элементу заказа может принаждлежать только один товар а каждома товару множество элементов заказа
        public int Quanity { get; set; }
        [Required]
        public Order order { get; set; }//при помощи этого свойтсва и ( св-ва items ) мы формируем отношение один ко многим в базе данных (а каждома элементу заказа только один заказ)

    }
}
