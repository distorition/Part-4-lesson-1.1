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

builder.Host.ConfigureContainer<ContainerBuilder>(container =>  //c ������� ����� ���������� ����� ��������� ����������� ��������� �������� ��� ������ �������� Autofac
{
    container.RegisterType<SqlOrderService>()//����� �� ������������ ��� ��������� �� � ������ ��������� ����� ����������
    .As<IOrderService>()//����� ��������� ������� �� ����������� � ������ 
    .InstancePerLifetimeScope();// � ����� ����� ����� �����������

    //  container.RegisterType<SqlOrderService>().InstancePerLifetimeScope();//(����� ������� ���� � ��� ���� �� ������������ �������� ������ ����������� �� ��� �� ��� ���� �� ������������)

    //container.RegisterModule<ServicesModule>();//����� ������� �� ���� ��������� ��� ���������������� ����� ������� 
    //container.RegisterAssemblyModules(Assembly.GetEntryAssembly()!);//�������� ����� ������� �� ������� ������ ( ������ �������� �������� ���� ����������)
    container.RegisterAssemblyModules(typeof(Program));//����� ������� � ����� ���� ����� ��������� ������ � ����� ��������� ��� ������ ��� ������ 

    var config = new ConfigurationBuilder()//������� ������ ������� ����� �������������� �� ������ ������ ������������ ( config)
    .AddJsonFile("autofac.config",false,false).Build();//����� ������� �� ���� ����� ��������� ��� ���� �������� ��� ����� ������ ������������ ( config) 

  //  container.RegisterModule(new ConfigurationModule((global::Divergic.Configuration.Autofac.IConfigurationResolver)config));

});


/// <summary>
/// ����� ���� ��� � ���������� WebApp �� ��������� ����������� ����� ���� ������ ��� ����� ����� ������� ������������ �������� 
/// </summary>

#region ����������� �������� ����������

var service = builder.Services;

var configuration = builder.Configuration;

service.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));//������������� ������ � ���� ������

//service.AddTransient<IOrderService, SqlOrderService>();//����������� ����������� ������ �������

#endregion

var app = builder.Build();

app.UseMiddleware<TestMidleWare>();//����� ������� �� �������� � ������� ���� ������������� ��
    
app.MapGet("/", () => "Hello World!");

UserManager<IdentityUser> userManager;//�������� ��� ��������� ����������

app.Run();
