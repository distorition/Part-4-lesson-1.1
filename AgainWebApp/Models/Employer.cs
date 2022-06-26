namespace AgainWebApp.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronomic { get; set; }

        public DateTime Birthday { get; set; }
    }
}
