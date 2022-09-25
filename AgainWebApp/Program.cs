using AgainWebApp.Infastructure.Autofac;
using AgainWebApp.Infastructure.MidleWare;
using AgainWebApp.Services;
using AgainWebApp.Services.Interfaces;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Divergic.Configuration.Autofac;
using Identity.DAL.Context;
using Identity.DAL.Entities;
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

    //var config = new ConfigurationBuilder()//создаем обьект который будет формироватьсяя на основе файлов конфигурации ( config)
    //.AddJsonFile("autofac.config",false,false).Build();//таким образом мы тоже можем загружать все наши ссервисы при помщи файлов конфигурации ( config) 

  //  container.RegisterModule(new ConfigurationModule((global::Divergic.Configuration.Autofac.IConfigurationResolver)config));

});


/// <summary>
/// после того как в преложения WebApp мы добавляем зависимость нашей базы данных нам нужно будет сделать конфигруацию сервисов 
/// </summary>

#region Кофигурация сервисов приложения

var service = builder.Services;

var configuration = builder.Configuration;

service.AddIdentity<User,Role>()
    .AddEntityFrameworkStores<IdentityDB>()
    .AddDefaultTokenProviders();//так мы подключаем систему Identity

service.Configure<IdentityOptions>(opt =>// вообще все это можно не добавлять ( тут мы можем настроить наш Identity)
{
#if DEBUG //таким образом мы делаем так чтобы все эти изменения работали только в режиме Debug

    opt.Password.RequireDigit = false;//таким образом мы снимаем ограничения на цыфры ( то есть они необязательны)
    opt.Password.RequireLowercase = false;
    opt.Password.RequiredLength = 3;// минимальная длинна пароля 

#endif

    opt.User.RequireUniqueEmail = false;// таким образом  отключили уникальный емаил  ( таким образом мы разрешили пользователям не указывать емаил)

    opt.Lockout.AllowedForNewUsers = false;//таким образом мы ращрешили новым пользователям не поддтверждать свой емаил по почте 

    opt.Lockout.MaxFailedAccessAttempts = 3;//таким образом указали максимальне кол-во попыток ввода пароля 

    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);// тут мы указали время блокировки при неудачной попытке ввода
});

service.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "AgainWebMVC";
    opt.Cookie.HttpOnly = true;//так печеньки будут передаваться только по каналу Http

   // opt.Cookie.Expiration = TimeSpan.FromDays(10);//таким образом мы указываем время существования печенек(устарела)
   opt.ExpireTimeSpan=TimeSpan.FromMinutes(10);//время жизни печеннек

    opt.LoginPath = "/Account/Login";//система авторизации будет посылать юзера по этому адресу если ему надо авторизироваться 
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";// в случаи отказа доступа по этому адресу 

    opt.SlidingExpiration = true;//таким образом разрешаем системе изменять индефикатор сессии когда юзер вошел или вышел ( это нужно для уменьшения вероятности атак)

});

service.AddControllersWithViews();//таким образом мы добавляем контроллеры вместо с представлениями ( то есть с визуальной частью Razor)


service.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));//конфигурируем доступ к базе данных

service.AddDbContext<IdentityDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("Identity")));
//service.AddTransient<IOrderService, SqlOrderService>();//стандартная регистрация нашего сервиса

//service.AddTransient<IEmployersStore, InMemoryEmploiesStore>();//так у нас содтрудники будут всегда одни и теже потому что изменения сохранятся не будут и создаваться они будут каждый раз заново 
//service.AddSingleton<IEmployersStore, InMemoryEmploiesStore>();// все изменения сохраняются только на время дейсвтия нашего контроллера , после завршения все изменения сбрасываются 

service.AddScoped<IEmployersStore, InMemoryEmploiesStore>();// тут наши изменения будут сохранятся в памяти 


#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();//желательно определять как можно раньше при постарении конвеера 

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


//app.UseMiddleware<TestMidleWare>();//таким образом мы доавляем в конвеер наше промежуточное ПО
    
//app.MapGet("/", () => "Hello World!");

//UserManager<IdentityUser> userManager;//алгоритм для обработки информации

app.MapDefaultControllerRoute();

app.Run();
