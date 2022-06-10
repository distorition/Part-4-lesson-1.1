using Microsoft.EntityFrameworkCore;
using Orders.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DAL.Context
{
    internal class OrdersDB:DbContext
    {
        public DbSet<Buyer> buyers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get; set; }
        public OrdersDB(DbContextOptions<OrdersDB> options) : base(options)
        {

        }
    }
}
