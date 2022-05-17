using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HomeWork.Lesson_4
{
    public class AddShopStructure
    {
        private List<ShopStructure> ShopStructure= new List<ShopStructure>();
        public ShopStructure Add(string Name, string Description, int Price)
        {
            var shop = new ShopStructure()
            {
                Name = Name,
                Description = Description,
                Price = Price
            };
            ShopStructure.Add(shop);
            return shop;
        }
        public ShopStructure Add(char Name, char Description, decimal Price)
        {
            var shop = new ShopStructure()
            {
                CharName = Name,
                CharDescription = Description,
                DecimalPrice = Price
            };
            ShopStructure.Add(shop);
            return shop;
        }

    }
}
