using AgainWebApp.Models;

namespace AgainWebApp.Services.Interfaces
{
    public interface IEmployersStore
    {
        IEnumerable<Employer> GetAll();
        Employer? GetByID(int id);//знак вопроса потому что может вернуться null если в списке нет такого id 
        int Add(Employer employer);
        bool Edit(Employer employer);
        bool Remove(int id);

    }
}
