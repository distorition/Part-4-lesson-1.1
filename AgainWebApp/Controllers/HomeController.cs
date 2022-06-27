using AgainWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgainWebApp.Controllers
{
    public class HomeController:Controller
    {
       
        private readonly ILogger<HomeController> _Logger;

        public HomeController(IEmployersStore employersStore, ILogger<HomeController> logger)//через конструктор в контроллер добавляются сервисы которыми он будет пользоваться 
        {
          
            _Logger = logger;//так же все наши свервисы помещаются в приватные поля 
        }

        /// <summary>
        /// Теперь мы будем определять действия контроллера ( пока у нас только одно действие Index)
        /// так же могут быть как асинхронные так и синхронные 
        /// лучше конечно использовать синхронные дейсвтия потому что тогда мы меньше расходуем ресурсов пула потоков 
        /// потому что приложение MVC пользуется пулом потока чтобы производить какие либо действия 
        /// и если у нас закончаться пулы потоков то какое либо дейсвтие станет в очередь и не будет обработано и  по итогу может отвалиться по таймуту 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
