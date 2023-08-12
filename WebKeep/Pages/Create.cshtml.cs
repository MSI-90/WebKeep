using Microsoft.AspNetCore.Mvc;
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
        public void OnPost()
        {
            if(ModelState.IsValid)
            {

            }
        }
        public class UserInputModel
        {
            [Required]
            [StringLength(20)]
            [Display(Name = "Категория")]
            public string Category { get; set; }

            [Required]
            [StringLength(50)]
            [Display(Name = "Описание")]
            public string Description { get; set; }

            [Required]
            [StringLength(250)]
            [Display(Name = "Ссылка на ресурс")]
            public string Link { get; set; }
            public string Date { get; set; } = DateTime.Now.ToString("dd.mm.yyyy");
        }
    }
}
