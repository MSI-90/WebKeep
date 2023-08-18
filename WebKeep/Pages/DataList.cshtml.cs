using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKeep.Interfaces;
using WebKeep.Models;

namespace WebKeep.Pages
{
    //� ������ ������ �������� �������������� ������ � ������ � ������� ��, 
    //� ����������� ������������
    public class DataListModel : PageModel
    {
        private readonly ISavedLinks _savedLinks;
        public List<DbModel> SavedLinksList { get; set; }
        public int Count { get; set; }
        public SelectListItem[] Categories { get; set; }

        [BindProperty]
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
            }
        }
        public class FilterSortModel
        {
            //[Category]
            public string Categories { get; set; }
        }
    }
}
