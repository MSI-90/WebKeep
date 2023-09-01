using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Drawing.Printing;
using WebKeep.Interfaces;
using WebKeep.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebKeep.Services;

namespace WebKeep.Pages
{
    //В данной модели страницы осуществляется доступ к данным в таблице БД, 
    //с последующим отображением
    public class DataListModel : PageModel
    {
        private readonly ISavedLinks _savedLinks;
        private int pageSize = 4;
        public PagingInfo PageInfo { get; set; }
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

        public void OnGet(int? elementPage)
        {
            var taskListResult = string.IsNullOrEmpty(InputSort.Categories)
            ? _savedLinks.GetDataAsync()
            : _savedLinks.GetAllDataFromCategory(InputSort);

            SavedLinksList = taskListResult.Result.ToList();
            Count = SavedLinksList?.Count ?? 0;

            for (int i = 0; i < Count; i++)
                SavedLinksList[i].IndexCount = i+1;

            PagingInfo pagInfo = new PagingInfo()
            {
                CurrentPage = elementPage ?? 1,
                ItemsPerPage = pageSize,
                TotalItems = Count
            };
            PageInfo = pagInfo;
            SavedLinksList = SavedLinksList.OrderBy(x => x.Date).Skip((pagInfo.CurrentPage - 1) * 
                pageSize).Take(pageSize).ToList();
        }
        public class FilterSortModel
        {
            public string Categories { get; set; }
        }
    }
}
