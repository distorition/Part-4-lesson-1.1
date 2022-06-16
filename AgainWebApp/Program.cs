using AgainWebApp.Infastructure.Autofac;
using AgainWebApp.Infastructure.MidleWare;
using AgainWebApp.Services;
using AgainWebApp.Services.Interfaces;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Divergic.Configuration.Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(container =>  //c помощью этого контейнера можно проводить регитсрацию различный сервисов при помощи сервисов Autofac
{
    container.RegisterType<SqlOrderService>()//когда мы регистрируем наш контейнер то в начале указываем класс реализации
    .As<IOrderService>()//потом интерфейс который мы реализовали в классе 
    .InstancePerLifetimeScope();// а потом время жизни регистрации

    //  container.RegisterType<SqlOrderService>().InstancePerLifetimeScope();//(таким образом если у нас было бы реализованно большего одного интрейрфеса то они бы все были бы реализованны)

    //container.RegisterModule<ServicesModule>();//таким образом мы либо указываем где зарегистрированы нашии сервисы 
    //container.RegisterAssemblyModules(Assembly.GetEntryAssembly()!);//загрузка наших модулей из текущей сборки ( откуда начинает работать наше приложение)
    container.RegisterAssemblyModules(typeof(Program));//таким образом у этого типа будет извлечена сборка и будут загружены все моудли это сборки 

    var config = new ConfigurationBuilder()//создаем обьект который будет формироватьсяя на основе файлов конфигурации ( config)
    .AddJsonFile("autofac.config",false,false).Build();//таким образом мы тоже можем загружать все наши ссервисы при помщи файлов конфигурации ( config) 

  //  container.RegisterModule(new ConfigurationModule((global::Divergic.Configuration.Autofac.IConfigurationResolver)config));

});


/// <summary>
/// после того как в преложения WebApp мы добавляем зависимость нашей базы данных нам нужно будет сделать конфигруацию сервисов 
/// </summary>

#region Кофигурация сервисов приложения

var service = builder.Services;

var configuration = builder.Configuration;

service.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));//конфигурируем доступ к базе данных

//service.AddTransient<IOrderService, SqlOrderService>();//стандартная регистрация нашего сервиса

#endregion

var app = builder.Build();

app.UseMiddleware<TestMidleWare>();//таким образом мы доавляем в конвеер наше промежуточное ПО
    
app.MapGet("/", () => "Hello World!");

UserManager<IdentityUser> userManager;//алгоритм для обработки информации

app.Run();
