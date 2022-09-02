using AgainWebApp.Models;
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

        public IActionResult Create()=>View("Edit",new EmployersViewModel());//вызывает персдтавления Edit и передаёт ему пустую вью модель


        // так же формируем модель представления Razor и шаблон указываем наш контроллер то есть ( EmployersCOntroller)
        //в самом шаблоне есть два действия которая будет отправлять форму  нашего человека на кнопку Edit и вторая  которая будет принимать 
        public IActionResult Edit(int? id )// отправляет 
        {
            if(id is not null)
            {
                return View(new EmployersViewModel());
            }
            var employer = _EmployersStore.GetByID((int)id);//ищем пользвоателя по ид

            if (employer is null)//если не нашли то посылаем ошибку 404 
            {
                return NotFound();
            }

            return View(new EmployersViewModel//если нашли то отправялем ему нашего чела которого будем редактирвоать 
            {
                Id = employer.Id,
                LastName = employer.LastName,
                FirstName = employer.FirstName,
                Patronomic = employer.Patronomic,
                Birthday = employer.Birthday,
            });//добавялем нашу вью модель
        }

        [HttpPost]
        public IActionResult Edit(EmployersViewModel model)// принимает ,принимаем модель которая уже отредактирвоана  
        {
            //var employer= _EmployersStore.GetByID(model.Id);
            //if (employer is null)//если не нашли то посылаем ошибку 404 
            //{
            //    return NotFound();
            //}

            var employee= new Employer
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronomic = model.Patronomic,
                Birthday = model.Birthday,
            };

            if(employee.Id== 0)//если ид=0 значит это новый сотрудник 
            {
                var id = _EmployersStore.Add(employee);
                return RedirectToAction("Details", new { id });
            }

            var succes = _EmployersStore.Edit(employee);

            if (!succes)
            {
                return NotFound();
            }

            return RedirectToAction("Index");//возвращаем редирект на дейсвие индекс 
        }

        public ActionResult Delete(int id)// первый запрос будет находить и формировать нашу модель 
        {
            var employer = _EmployersStore.GetByID(id);//ищем пользвоателя по ид

            if (employer is null)//если не нашли то посылаем ошибку 404 
            {
                return NotFound();
            }


            return View(new EmployersViewModel// тут мы ввозвращаем нашу сформированную модель 
            {
                Id = employer.Id,
                LastName = employer.LastName,
                FirstName = employer.FirstName,
                Patronomic = employer.Patronomic,
                Birthday = employer.Birthday,
            });
        }

        [HttpPost]//тут нужен пост запрос для безопасности чтобы никто не смог взломать и удалиить наших сотрудников 
        public ActionResult DeleteConfirmed(int id)//второй будет принимать эту модель и удалять её
        {

            //var employer = _EmployersStore.GetByID(id);//ищем пользвоателя по ид

            //if (employer is null)//если не нашли то посылаем ошибку 404 
            //{
            //    return NotFound();
            //}

            //_EmployersStore.Remove(id);

            if (!_EmployersStore.Remove(id))//если не удалось удалить значит он не был найден 
            {
                return NotFound();
            }


            return RedirectToAction("Index");//а если удалось удалить то возвращаем редирект 
        }
    }
}
