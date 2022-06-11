using AgainWebApp.Infastructure.MidleWare;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(container =>  //c помощью этого контейнера можно проводить регитсрацию различный сервисов при помощи сервисов Autofac
{

});


/// <summary>
/// после того как в преложения WebApp мы добавляем зависимость нашей базы данных нам нужно будет сделать конфигруацию сервисов 
/// </summary>

#region Кофигурация сервисов приложения

var service = builder.Services;

var configuration = builder.Configuration;

service.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));//конфигурируем доступ к базе данных

#endregion

var app = builder.Build();

app.UseMiddleware<TestMidleWare>();//таким образом мы доавляем в конвеер наше промежуточное ПО
    
app.MapGet("/", () => "Hello World!");

UserManager<IdentityUser> userManager;//алгоритм для обработки информации

app.Run();
