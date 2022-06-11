using AgainWebApp.Infastructure.MidleWare;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(container =>  //c ������� ����� ���������� ����� ��������� ����������� ��������� �������� ��� ������ �������� Autofac
{

});


/// <summary>
/// ����� ���� ��� � ���������� WebApp �� ��������� ����������� ����� ���� ������ ��� ����� ����� ������� ������������ �������� 
/// </summary>

#region ����������� �������� ����������

var service = builder.Services;

var configuration = builder.Configuration;

service.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));//������������� ������ � ���� ������

#endregion

var app = builder.Build();

app.UseMiddleware<TestMidleWare>();//����� ������� �� �������� � ������� ���� ������������� ��
    
app.MapGet("/", () => "Hello World!");

UserManager<IdentityUser> userManager;//�������� ��� ��������� ����������

app.Run();
