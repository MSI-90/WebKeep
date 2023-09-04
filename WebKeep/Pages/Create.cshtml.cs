using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebKeep.Interfaces;

namespace WebKeep.Pages
{
    public class Index2Model : PageModel
    {
        private readonly ISavedLinks _savedLinks;

        [BindProperty]
        public UserInputModel UserInput { get; set; }
        public SelectListItem[] Categories { get; set; }

        [BindProperty]
        public InputCategoryUser InputCategory { get; set; }
        public Index2Model(ISavedLinks savedLinks)
        {
            _savedLinks = savedLinks;
            var categoryResult = _savedLinks.GetCategory();
            if (categoryResult != null)
                Categories = categoryResult.Result;
        }
        public void OnGet()
        {
            ViewData["Title"] = "Добавление новой записи";
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _savedLinks.AddNewItemInSavedLinks(UserInput);
                    if (result.Result != 1)
                    {
                        return RedirectToPage("NotFound", new
                        {
                            error = "Невозможно добавить запись в список, возникла ошибка!"
                        });
                    }
                    return RedirectToPage("DataList");
                }
                catch (System.AggregateException)
                {
                    return RedirectToPage("NotFound", new
                    {
                        error = "Ошибка в синтаксисе запроса SQL, возникла ошибка!"
                    });
                }
            }
            else
            {
                return Page();
            }
            
        }
        public class InputCategoryUser
        {
            [Display(Name = "Имеющиеся категории")]
            public string Category { get; set; }
        }
        public class UserInputModel
        {
            [Required]
            [StringLength(50)]
            [Display(Name = "Категория")]
            public string? Category { get; set; }

            [Required]
            [StringLength(200)]
            [Display(Name = "Описание")]
            public string Description { get; set; }

            [Required]
            [StringLength(1000)]
            [Display(Name = "Ссылка на ресурс")]
            public string Link { get; set; }
            public string Date { get; set; } = DateTime.Now.ToString("dd.MM.yyyy");
        }
    }
}
