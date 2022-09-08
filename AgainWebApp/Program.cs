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

builder.Host.ConfigureContainer<ContainerBuilder>(container =>  //c ������� ����� ���������� ����� ��������� ����������� ��������� �������� ��� ������ �������� Autofac
{
    container.RegisterType<SqlOrderService>()//����� �� ������������ ��� ��������� �� � ������ ��������� ����� ����������
    .As<IOrderService>()//����� ��������� ������� �� ����������� � ������ 
    .InstancePerLifetimeScope();// � ����� ����� ����� �����������

    //  container.RegisterType<SqlOrderService>().InstancePerLifetimeScope();//(����� ������� ���� � ��� ���� �� ������������ �������� ������ ����������� �� ��� �� ��� ���� �� ������������)

    //container.RegisterModule<ServicesModule>();//����� ������� �� ���� ��������� ��� ���������������� ����� ������� 
    //container.RegisterAssemblyModules(Assembly.GetEntryAssembly()!);//�������� ����� ������� �� ������� ������ ( ������ �������� �������� ���� ����������)
    container.RegisterAssemblyModules(typeof(Program));//����� ������� � ����� ���� ����� ��������� ������ � ����� ��������� ��� ������ ��� ������ 

    //var config = new ConfigurationBuilder()//������� ������ ������� ����� �������������� �� ������ ������ ������������ ( config)
    //.AddJsonFile("autofac.config",false,false).Build();//����� ������� �� ���� ����� ��������� ��� ���� �������� ��� ����� ������ ������������ ( config) 

  //  container.RegisterModule(new ConfigurationModule((global::Divergic.Configuration.Autofac.IConfigurationResolver)config));

});


/// <summary>
/// ����� ���� ��� � ���������� WebApp �� ��������� ����������� ����� ���� ������ ��� ����� ����� ������� ������������ �������� 
/// </summary>

#region ����������� �������� ����������

var service = builder.Services;

var configuration = builder.Configuration;

service.AddIdentity<User,Role>()
    .AddEntityFrameworkStores<IdentityDB>()
    .AddDefaultTokenProviders();//��� �� ���������� ������� Identity

service.Configure<IdentityOptions>(opt =>// ������ ��� ��� ����� �� ��������� ( ��� �� ����� ��������� ��� Identity)
{
#if DEBUG //����� ������� �� ������ ��� ����� ��� ��� ��������� �������� ������ � ������ Debug

    opt.Password.RequireDigit = false;//����� ������� �� ������� ����������� �� ����� ( �� ���� ��� �������������)
    opt.Password.RequireLowercase = false;
    opt.Password.RequiredLength = 3;// ����������� ������ ������ 

#endif

    opt.User.RequireUniqueEmail = false;// ����� �������  ��������� ���������� �����  ( ����� ������� �� ��������� ������������� �� ��������� �����)

    opt.Lockout.AllowedForNewUsers = false;//����� ������� �� ��������� ����� ������������� �� ������������� ���� ����� �� ����� 

    opt.Lockout.MaxFailedAccessAttempts = 3;//����� ������� ������� ����������� ���-�� ������� ����� ������ 

    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);// ��� �� ������� ����� ���������� ��� ��������� ������� �����
});

service.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "AgainWebMVC";
    opt.Cookie.HttpOnly = true;//��� �������� ����� ������������ ������ �� ������ Http

   // opt.Cookie.Expiration = TimeSpan.FromDays(10);//����� ������� �� ��������� ����� ������������� �������(��������)
   opt.ExpireTimeSpan=TimeSpan.FromMinutes(10);//����� ����� ��������

    opt.LoginPath = "/Account/Login";//������� ����������� ����� �������� ����� �� ����� ������ ���� ��� ���� ���������������� 
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";// � ������ ������ ������� �� ����� ������ 

    opt.SlidingExpiration = true;//����� ������� ��������� ������� �������� ����������� ������ ����� ���� ����� ��� ����� ( ��� ����� ��� ���������� ����������� ����)

});

service.AddControllersWithViews();//����� ������� �� ��������� ����������� ������ � ��������������� ( �� ���� � ���������� ������ Razor)


service.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));//������������� ������ � ���� ������

service.AddDbContext<IdentityDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("Identity")));
//service.AddTransient<IOrderService, SqlOrderService>();//����������� ����������� ������ �������

//service.AddTransient<IEmployersStore, InMemoryEmploiesStore>();//��� � ��� ����������� ����� ������ ���� � ���� ������ ��� ��������� ���������� �� ����� � ����������� ��� ����� ������ ��� ������ 
//service.AddSingleton<IEmployersStore, InMemoryEmploiesStore>();// ��� ��������� ����������� ������ �� ����� �������� ������ ����������� , ����� ��������� ��� ��������� ������������ 

service.AddScoped<IEmployersStore, InMemoryEmploiesStore>();// ��� ���� ��������� ����� ���������� � ������ 


#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();//���������� ���������� ��� ����� ������ ��� ���������� �������� 

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


//app.UseMiddleware<TestMidleWare>();//����� ������� �� �������� � ������� ���� ������������� ��
    
//app.MapGet("/", () => "Hello World!");

//UserManager<IdentityUser> userManager;//�������� ��� ��������� ����������

app.MapDefaultControllerRoute();

app.Run();
