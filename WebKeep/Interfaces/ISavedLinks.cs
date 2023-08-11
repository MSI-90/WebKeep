using WebKeep.Models;
using static WebKeep.Pages.TestPModel;

namespace WebKeep.Interfaces
{
    //Интерфейс, реализующий CRUD 

    public interface ISavedLinks
    {
        //public int Count { get; set; }
        Task<List<DbModel>> GetDataAsync();
        Task<DbModel> GetSavedLinks(int id);
        Task<int> UpdateSavedLinks(UserEditModel model, int id);
        Task<int> DeleteSavedLinks(int id);
    }
}
