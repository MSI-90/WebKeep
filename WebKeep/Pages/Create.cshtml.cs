using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebKeep.Interfaces;

namespace WebKeep.Pages
{
    public class Index2Model : PageModel
    {
        private readonly ISavedLinks _savedLinks;

        [BindProperty]
        public UserInputModel UserInput { get; set; }
        public Index2Model(ISavedLinks savedLinks)
        {
            _savedLinks = savedLinks;
        }
        public void OnGet()
        {
            ViewData["Title"] = "Добавление новой записи";
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Ошибка");
                return Page();
            }
            else 
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
        }
        public class UserInputModel
        {
            [Required]
            [StringLength(50)]
            [Display(Name = "Категория")]
            public string Category { get; set; }

            [Required]
            [StringLength(200)]
            [Display(Name = "Описание")]
            public string Description { get; set; }

            [Required]
            [StringLength(500)]
            [Display(Name = "Ссылка на ресурс")]
            public string Link { get; set; }
            public string Date { get; set; } = DateTime.Now.ToString("dd.MM.yyyy");
        }
    }
}
