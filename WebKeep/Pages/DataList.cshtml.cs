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
        public SelectListItem[] Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public FilterSortModel InputSort { get; set; }
        public DataListModel(ISavedLinks savedLinks)
        {
            _savedLinks = savedLinks;

            var categoryResult = _savedLinks.GetCategory();
            if (categoryResult != null)
            {
                Categories = categoryResult.Result;
            }
        }

        public void OnGet()
        {
            var taskListResult = string.IsNullOrEmpty(InputSort.Categories)
            ? _savedLinks.GetDataAsync()
            : _savedLinks.GetAllDataFromCategory(InputSort);

            if (taskListResult != null)
            {
                SavedLinksList = taskListResult.Result?.ToList();
                Count = SavedLinksList?.Count ?? 0;
            }
        }
        
        public class FilterSortModel
        {
            public string Categories { get; set; }
        }
    }
}
