using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();//������� ������� ( �������)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())// ��� �� ������������� ������� ��� ����� �������(DEBUG)
                                    // ���� ����� ������� �� ��������� � launchSitings �� AspnNetEnvirnoment � ����� ��������� ��� ���������� � Development ��� REalise
{
    app.UseDeveloperExceptionPage();//����� ������� ���� � ��� ����� ������ �� �� ����� �������� Html �������� � ������������ ������ 
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();//����� �������� ��� ���� �������� ���� ( ��� ������� 7045), �� ����� ����� �������������� ��� �������� Https �� ����������� ������ 

app.UseStaticFiles();// ����� ��������� ��������� ����� �� ����� wwwroot

app.UseRouting();//��������� ������� ������������� , ������� ��������� ������ �� �������� 

app.UseAuthorization();
app.UseAuthentication();//��� ������ ����� ��� ������ � Identity  ( ��� ������ ���� �������� �������������� ������������)

app.MapDefaultControllerRoute();//��� �� �� ����� ���������� ������� �� ��������� (����������� ����� �� ������� ��� ��� ������� �������� ����)

app.MapControllerRoute(//��� �������� ������� ������ ���������� 
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");// ���������� ��� ����������� � �������� ������ , ��� �� ��������� ����������(Controlle=Home),��� �� ����� ������ (action=Index)������ ����� ������� ����� ���������
                                                       // ���� ���� �� ���������� ���� ����� ������������ �� ������ ������ ������ 404
                                                       // ��� �� �������������� ��������� �� ���� id ������� ����� ������������ ������� ������ ���� ������� id?
app.Run();
