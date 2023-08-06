using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebKeep.Interfaces;
using WebKeep.Models;

namespace WebKeep.Pages
{
    //В данной модели страницы осуществляется доступ к данным в таблице БД, 
    //с последующим отображением
    public class DataListModel : PageModel
    {
        private readonly ISavedLinks _savedLinks;
        public List<DbModel> SavedLinksList { get; set; }
        public int Count { get; set; }
        public DataListModel(ISavedLinks savedLinks)
        {
            _savedLinks = savedLinks;
        }

        public void OnGet()
        {
            if (ModelState.IsValid)
            {
                var taskListResult = _savedLinks.GetDataAsync();
                if (taskListResult != null)
                {
                    SavedLinksList = taskListResult.Result;
                    Count = SavedLinksList.Count;
                }
            }
        }
    }
}
