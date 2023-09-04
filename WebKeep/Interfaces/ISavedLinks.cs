using Microsoft.AspNetCore.Mvc.Rendering;
using WebKeep.Models;
using static WebKeep.Pages.DataListModel;
using static WebKeep.Pages.Index2Model;
using static WebKeep.Pages.TestPModel;

namespace WebKeep.Interfaces
{
    //Интерфейс, реализующий CRUD 

    public interface ISavedLinks
    {
        Task<IEnumerable<DbModel>> GetDataAsync();
        Task<DbModel> GetSavedLinks(int id);
        Task<int> UpdateSavedLinks(UserEditModel model, int id);
        Task<int> DeleteSavedLinks(int id);
        Task<int> AddNewItemInSavedLinks(UserInputModel model, InputCategoryUser input);
        Task<SelectListItem[]> GetCategory();
        Task<IEnumerable<DbModel>> GetAllDataFromCategory(FilterSortModel sort);
    }
}
