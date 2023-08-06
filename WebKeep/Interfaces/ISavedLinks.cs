using WebKeep.Models;

namespace WebKeep.Interfaces
{
    //Интерфейс, реализующий CRUD 

    public interface ISavedLinks
    {
        Task<List<DbModel>> GetDataAsync();
        Task<DbModel> GetSavedLinks(int id);
    }
}
