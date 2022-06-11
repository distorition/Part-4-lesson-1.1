
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.DAL.Context;

namespace Orders.ConsoleUI
{
    class Program
    {
        private static IHost _Host;

        public static IHost Hosting => _Host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();//передаем параметры команндной строки и после этого вызываем метод BUild чтобы посторить наш хост
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)//тут мы создаем наш хост 
            .ConfigureHostConfiguration(opt => opt.AddJsonFile("appsetings.json"))//сюда можем добавить файлы конфигурации ( json) так в же в файла конфига хранятся строки подключения 
            .ConfigureAppConfiguration(opt => opt.AddJsonFile("appsetings.json"))//и таким образом мы можем добавлять наши конфигурации столько сколько нам надо
            .ConfigureServices(Configureservices);
        private static void Configureservices(HostBuilderContext host,IServiceCollection service)//этам конфигурирования сервисов 
        {
            //сюда мы добавляем нашуу базу данных
            service.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("SqlServer")));

        }
        public static async Task Main(string[] args)
        {
            var host = Hosting;
            await host.StartAsync();//запускаем наш хост
            Console.ReadLine();
            await host.StopAsync();// останавливаем
        }

    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orders.DAL.Context;
using Orders.DAL.Entities;

#region Использование Базы Данных без контейнера
// при работе с базой данных
var db_options = new DbContextOptionsBuilder<OrdersDB>()// сперва мы формируем DbContextOptionsBuilder
    .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrdersDB");// методом UseSqlServer мы указываем что мы будем использовать именно SqlServer и указываем строку подключения 

using (var db = new OrdersDB(db_options.Options))
{
    //и вот здесь мы уже работаем с базой данных

    db.Database.EnsureCreated();//нужно для создания нашей базы данных на нашем сервере 

    if (!db.buyers.Any())//ессли нет ниодного покупателя
    {
        //добавляем покупателей в базу данных
        db.buyers.Add(new Buyer
        {

            Birthday = DateTime.Now.AddYears(-18),
            LastName = "Ivanov",
            Name = "Ivan",
            Patronomic = "Ivanovich",
        });

        db.buyers.Add(new Buyer
        {

            Birthday = DateTime.Now.AddYears(-13),
            LastName = "Петров",
            Name = "Петр",
            Patronomic = "Петрович",
        });

        db.buyers.Add(new Buyer
        {

            Birthday = DateTime.Now.AddYears(-22),
            LastName = "Смирнов",
            Name = "Алексей",
            Patronomic = "Алексеевич",
        });

        db.SaveChanges();//после того как добавили мы должны сохранить изменения 
    }

}
#endregion

var services_collestion = new ServiceCollection();
//services_collestion.AddSingleton<IService, ServiceImplementation>();//способ для регитсрации нашего сервеса <Интерфейс Сервеса, Реализация интерйеса>

services_collestion.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrdersDB"));//так мы доваляем в наши сервисы нашу базу данных(формируем контейнер)

var servvice_provaider = services_collestion.BuildServiceProvider();//из  коллекции мы можем сформировать провайдер
//servvice_provaider.GetRequiredService<OrdersDB>();//чтобы потом из него получать нужные нам сервисы

var service_db = servvice_provaider.GetRequiredService<OrdersDB>(); //using мы не используем тут потому что провайдер сервеса сам регулирует время жизни обьекта 

foreach (var buyer in service_db.buyers)//а тут мы получаем сведения об наших покупателей которые мы добавили в базу данных
{
    Console.WriteLine("{0} {1} {2}-{3}", buyer.Name, buyer.LastName, buyer.Birthday, buyer.Patronomic);
}
Console.WriteLine("Hello, World!");
