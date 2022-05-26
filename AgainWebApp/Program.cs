using AgainWebApp.Infastructure.MidleWare;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<TestMidleWare>();//����� ������� �� �������� � ������� ���� ������������� ��
    
app.MapGet("/", () => "Hello World!");

UserManager<IdentityUser> userManager;//�������� ��� ��������� ����������

app.Run();
