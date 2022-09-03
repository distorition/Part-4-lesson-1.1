namespace AgainWebApp.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = null!;// то есть это значения не может иметь null то есть обязательна для заполнения
        public string? LastName { get; set; } //знак вопроса значит если после прохождения валидации в этом значении ничего нет значит данные корректные 
        public string? Patronomic { get; set; }

        public int Age { get; set; }
    }
}
