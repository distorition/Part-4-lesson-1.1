using Orders.DAL.Entities;

namespace AgainWebApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(string BuyerName, string Adress, string Phone, IEnumerable<(Product product, int Quantity)> products); 
    }
}
