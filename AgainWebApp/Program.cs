using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

UserManager<IdentityUser> userManager;//�������� ��� ��������� ����������

app.Run();
