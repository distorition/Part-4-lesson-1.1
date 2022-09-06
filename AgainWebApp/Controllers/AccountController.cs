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

        #region Регистрация
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

        public IActionResult LogIn() => View();
        public IActionResult LogOut() => View();
        public IActionResult AcessDenied() => View();

    }
}
