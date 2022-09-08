using AgainWebApp.Services.ViewsModels.Identity;
using Identity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgainWebApp.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<User> _User;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> User,SignInManager<User> signInManager,ILogger<AccountController> Logger)
        {
            _User = User;
            _SignInManager = signInManager;
            _Logger = Logger;
        }

        #region РЕГИСТРАЦИЯ
        public IActionResult Register()=>View(new RegisterUserViewModel());//отплавляет пустую вью модель

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)//принимает эту модель
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
            };

            var result = await _User.CreateAsync(user, model.Password);//создаем пользователя , так же хешируем его пароль ( нельзя забывать сюда его передавать)

            if (result.Succeeded)// првоерка успешно ли был он создан
            {
                _Logger.LogInformation("Пользователь{0} создан", user);

                await _SignInManager.SignInAsync(user, false);//и если успешно то логиним нашего пользователя ( false значит временно)

                return RedirectToAction("Index", "Home");//и перенаправляем  на главную сраницу 
            }

            foreach(var error in result.Errors)//тут мы добавляем все наши ошибки в случаи неудачной регистрации 
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        #endregion

        #region ВХОД В СИСТЕМУ
        //отправялем модель
        public IActionResult LogIn(string? ReturnUrl) => View( new LoginViewModel { ReturnUrl= ReturnUrl });// обычно Url возвращает сама система когда мы выполняем логин 

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)//принимаем модель
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            var resut = await _SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);//тут наша авторизация ( проверям имя , пароль , запоминать нас или нет , и true это блокировать ли нас в случае недуачной попытки входа)     

            if (resut.Succeeded)//толь если есть await мы сможем вызвать Succeeded
            {
                _Logger.LogInformation("Пользователь{0} вошел",model.UserName);// при логировании так делать нельзя $("Пользователь{model.UserName} вошел",)

                //if(model.ReturnUrl is { Length:>0} return_url) //проверка на то есть ли у нас url и если есть то мы по нему переходим   
                //{
                //    return LocalRedirect(return_url);
                //}

                //return Redirect(model.ReturnUrl); ОПАСНО,  пользователя могут отправить туда куда не надо

                //LocalRedirect это перенаправления в пределах вашего адреса чтобы пользователя не потравили куда то на внешние непонятные сайты 
                return LocalRedirect(model.ReturnUrl??"/");//model.ReturnUrl??"/") по сути такая же проверка как сверху ( если нет юрл то перееходим на корень ) 

            }
            ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            _Logger.LogWarning("Ошибка при входе в систему{0}", model.UserName);

            return View(model);//и отправляем обратно на представление 

        }
        #endregion

        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public IActionResult AcessDenied() => View();

    }
}
