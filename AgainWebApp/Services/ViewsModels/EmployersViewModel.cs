using Microsoft.AspNetCore.Mvc;

namespace AgainWebApp.Services.ViewsModels
{
    public class EmployersViewModel
    {
        [HiddenInput(DisplayValue =false)]//таким образом мы скрываем id человека от пользователей 
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronomic { get; set; }

        public DateTime Birthday { get; set; }
    }
}
