using AgainWebApp.Models;
using AgainWebApp.Services.Interfaces;

namespace AgainWebApp.Services
{
    public class InMemoryEmploiesStore:IEmployersStore
    {
        private readonly List<Employer> _Employers;
        private readonly ILogger<InMemoryEmploiesStore> _Logger;
        private int MaxFreeid;

        public InMemoryEmploiesStore(ILogger<InMemoryEmploiesStore> logger)
        {
            //_Employers = new List<Employer>()
            //{
            //    new Employer() { Id=1,FirstName ="Alex", LastName="Ass",}
            //};

            _Employers=Enumerable.Range(1,10).Select(i => new Employer()//берем 10 сотрудников
            {
                Id = i,
                LastName =$"Фамилия-{i}",
                FirstName =$"Имя-{i}",
                Patronomic =$"Отчество-{i}",
                Age =18+i,
            }).ToList();
            _Logger = logger;
            MaxFreeid=_Employers.Max(x=>x.Id)+1;
        }

        public int Add(Employer employer)
        {
            employer.Id = MaxFreeid;
            MaxFreeid++;
            _Employers.Add(employer);
            return employer.Id;

        }

        public bool Edit(Employer employer)
        {
            var db_item= GetByID(employer.Id);
            if (db_item is null)
            {
                return false;
            }
            db_item.LastName=employer.LastName;
            db_item.FirstName=employer.FirstName;
            db_item.Patronomic=employer.Patronomic;
            db_item.Age=employer.Age;

            //SaveChange() это если мы добавялем в базу данных

            return true;
        }

        public Employer? GetByID(int id)
        {
          return _Employers.FirstOrDefault(x => x.Id == id);//если будем брать из базы данных то вместо Employers указываем нашу таблицу откуда берем значения
        }

        public IEnumerable<Employer> GetAll()
        {
            return _Employers;//возвращаем весь список сотрудников
        }

        public bool Remove(int id)
        {
            var db_item = GetByID(id);
            if (db_item is null)
            {
                return false;
            }
            _Employers.Remove(db_item);
            return true;
        }
    }
}
