using AgainWebApp.Infastructure.MidleWare;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

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
