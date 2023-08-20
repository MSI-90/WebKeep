using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public bool IsFilterOnCategory { get; set; } = false;
        public SelectListItem[] Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public FilterSortModel InputSort { get; set; }
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
                var categoryResult = _savedLinks.GetCategory();
                if (categoryResult != null)
                {
                    Categories = categoryResult.Result;
                }

                if (!string.IsNullOrEmpty(InputSort.Categories))
                {
                    var result = _savedLinks.GetAllDataFromCategory(InputSort);
                    if (result != null)
                    {
                        SavedLinksList = result.Result;
                        IsFilterOnCategory = true;
                        Count = SavedLinksList.Count;
                    }
                }
                
            }
        }
        
        public class FilterSortModel
        {
            //[Category]
            public string Categories { get; set; } = string.Empty;
        }
    }
}
