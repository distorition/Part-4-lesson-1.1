using AgainWebApp.Infastructure.MidleWare;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<TestMidleWare>();//таким образом мы доавляем в конвеер наше промежуточное ПО
    
app.MapGet("/", () => "Hello World!");

UserManager<IdentityUser> userManager;//алгоритм для обработки информации

app.Run();
