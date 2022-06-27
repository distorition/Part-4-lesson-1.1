using AgainWebApp.Services.Interfaces;
using AgainWebApp.Services.ViewsModels;
using Microsoft.AspNetCore.Mvc;

namespace AgainWebApp.Controllers
{
    public class EmployersCOntroller:Controller
    {
        private readonly IEmployersStore _EmployersStore;
        private readonly ILogger<EmployersCOntroller> _Logger;

        public EmployersCOntroller(IEmployersStore employersStore, ILogger<EmployersCOntroller> logger)
        {
            _EmployersStore=employersStore;
            _Logger = logger;
        }
        public IActionResult Index()
        {
             var employers = _EmployersStore.GetAll();//таким образом мы получаем всех наших сотрудников из нашего сервиса 
            return View(employers);
        }

        public IActionResult Details(int id)
        {
            var employer= _EmployersStore.GetByID(id);
            if(employer is null)
            {
                return NotFound();
            }

            return View(new EmployersViewModel
            {
                Id= employer.Id,
                LastName = employer.LastName,
            FirstName = employer.FirstName,
            Patronomic = employer.Patronomic,
            Birthday = employer.Birthday,
        });//добавялем нашу вью модель
        }

        public IActionResult Create()=>View();
        public IActionResult Edit() => View();
        public ActionResult Delete() => View();

    }
}
