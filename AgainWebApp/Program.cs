using AgainWebApp.Infastructure.MidleWare;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

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
