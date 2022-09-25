using System.ComponentModel.DataAnnotations;

namespace AgainWebApp.Services.ViewsModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name ="Имя пользователя")]
        public string UserName { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }  =null!;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждения пароля")]
        [Compare(nameof(Password), ErrorMessage ="Пароли не совпдают")]//нужен нам для сравнения двух паролей 
        public string PasswordComfirmed { get; set; } = null!;
    }
}
