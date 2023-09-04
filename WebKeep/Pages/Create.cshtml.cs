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
            ViewData["Title"] = "���������� ����� ������";
        }
        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError(String.Empty, "������");
            //    return Page();
            //}
            //else 
            //{
                try
                {
                    var result = _savedLinks.AddNewItemInSavedLinks(UserInput, InputCategory);
                    if (result.Result != 1)
                    {
                        return RedirectToPage("NotFound", new
                        {
                            error = "���������� �������� ������ � ������, �������� ������!"
                        });
                    }
                    return RedirectToPage("DataList");
                }
                catch (System.AggregateException)
                {
                    return RedirectToPage("NotFound", new
                    {
                        error = "������ � ���������� ������� SQL, �������� ������!"
                    });
                }
            //}
        }
        public class InputCategoryUser
        {
            [Display(Name = "��������� ���������")]
            public string Category { get; set; }
        }
        public class UserInputModel
        {
            //[Required]
            [StringLength(50)]
            [Display(Name = "���������")]
            public string? Category { get; set; }

            [Required]
            [StringLength(200)]
            [Display(Name = "��������")]
            public string Description { get; set; }

            [Required]
            [StringLength(500)]
            [Display(Name = "������ �� ������")]
            public string Link { get; set; }
            public string Date { get; set; } = DateTime.Now.ToString("dd.MM.yyyy");
        }
    }
}
