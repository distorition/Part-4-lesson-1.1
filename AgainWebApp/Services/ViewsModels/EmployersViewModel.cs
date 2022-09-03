using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgainWebApp.Services.ViewsModels
{
    public class EmployersViewModel:IValidatableObject// модель представления нужна чтобы показать как визуализаировать данные 
    {
        [HiddenInput(DisplayValue =false)]//таким образом мы скрываем id человека от пользователей 
        public int Id { get; set; }
        [Required(ErrorMessage ="Имя является обязательным ")]
        [Display(Name = "Имя")]// так мы показываем как будут отображаться эти поля 
        [RegularExpression(@"(^[А-ЯЁ][а-яё\-0-9]+$)|([A-Z][a-z]+)",ErrorMessage ="Строка имеет неверные символы")]
        [StringLength(16,MinimumLength =2)]//таким образом мы устанавливаем макссимальное и минимальное  кол-во символов  
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "Фамилия является обязательным ")]                            
        [Display(Name ="Фамилия")] 
        [StringLength (16,MinimumLength =2)]
        [RegularExpression(@"(^[А-ЯЁ][а-яё\-0-9]+$)|([A-Z][a-z]+)", ErrorMessage = "Строка имеет неверные символы")]
        public string LastName { get; set; }  
       
        [Display(Name = "Отчество")]
        [StringLength(19,MinimumLength =2)]
        [RegularExpression(@"(^[А-ЯЁ][а-яё\-0-9]+$)|([A-Z][a-z]+)", ErrorMessage = "Строка имеет неверные символы")]
        public string Patronomic { get; set; }

        [Required(ErrorMessage = "Даиа рождения является обязательным ")]// при помощи ErroMessage мы сможем указать какое сообщение будет выводиться в случае ошибки
        [Display(Name = "Дата рождения")]
        [Range(18,85,ErrorMessage ="Вы должны быть страше 18")]// таким образом мы задаем диапазон значений нашего возраста 
        public int Age { get; set; }

        [DataType(DataType.Password)]//таким образом мы просто указываем какой тип данных то есть если у нас это пароль то он скроет их за звездочками
        public int Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success!;
        }
    }
}
