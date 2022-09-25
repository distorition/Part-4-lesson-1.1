using AgainWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;
using Orders.DAL.Entities;

namespace AgainWebApp.Services
{
    public class SqlOrderService : IOrderService
    {
        private readonly OrdersDB _dB;
        private readonly ILogger<SqlOrderService> _logger;

        public SqlOrderService(OrdersDB dB, ILogger<SqlOrderService> logger)
        {
            _dB = dB;
            _logger = logger;
        }

        public async Task<Order> CreateAsync(string BuyerName,string Adress,string Phone, IEnumerable<(Product product, int Quantity)> products)
        {
            var buyer = await _dB.buyers.FirstOrDefaultAsync(b => b.Name == BuyerName);//ищем заказчика по его имени 

            if(buyer is null)//если покупателя не нашли то добавялем его
            {
                await _dB.AddAsync(buyer=new Buyer { Name = BuyerName, Birthday = DateTime.Now });
                await _dB.SaveChangesAsync();
            }

            var order = new Order //создаем заказ
            {
                buyer = buyer,
                Adress = Adress,
                Phonee = Phone,
                Date = DateTime.Now,
                Items = products.Select(p => new Orderitem // так мы получаем коллекцию товара наш пользователь добавлял 
                {
                    product=p.product,
                    Quanity=p.Quantity,

                }).ToArray()
            };
            await _dB.orders.AddAsync(order);
            await _dB.SaveChangesAsync();
            return order;
        }
    }
}
