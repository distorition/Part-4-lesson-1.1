using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();//создаем конвеер ( фабрику)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())// тут мы конфигурируем конвеер для режим отладки(DEBUG)
                                    // этот метод смотрит на переменну в launchSitings на AspnNetEnvirnoment в каком состоянии она находиться в Development или REalise
{
    app.UseDeveloperExceptionPage();//таким образом если у нас будет ошибка то на сайте выскачит Html страница с отображением ошибки 
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();//метод доавляет еще один открытый порт ( это который 7045), он нужен чтобы перенаправлять все запоросы Https по защищенному каналу 

app.UseStaticFiles();// метод позволяет загружать файлы из папки wwwroot

app.UseRouting();//применяет систему маршрутизации , которая извлекает данные из маршрута 

app.UseAuthorization();
app.UseAuthentication();//оба метода нужны при работе с Identity  ( для какого либо спосообо индентификации пользователя)

app.MapDefaultControllerRoute();//так же мы можем определить маршрут по умолчанию (прописывает такой же муршрут как тот который прописан ниже)

app.MapControllerRoute(//тут пропасин маршрут нашего приложения 
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");// определяем три перемменных в даресной строке , так мы определям контроллер(Controlle=Home),что он будет делать (action=Index)точнее метод который будет выполнять
                                                       // если один из параметров пути будет отсуствовать то сервер вернет ошибку 404
                                                       // так же необязательные параметры по типу id который может отсуствовать поэтому ставим знак вопроса id?
app.Run();
