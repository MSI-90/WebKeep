using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using WebKeep.Interfaces;
using WebKeep.Models;

namespace WebKeep.Pages
{
    public class TestPModel : PageModel
    {
        private readonly ISavedLinks _savedLinks;
        public DbModel SavedLinks { get; set; }
        public UserEditModel Input { get; set; }
        public TestPModel(ISavedLinks savedLinks)
        {
            _savedLinks = savedLinks;
        }
        public IActionResult OnGet(int id)
        {
            var result = GetDbModelAsync(id);

            if (result.Result is null)
            {
                return RedirectToPage("NotFound", new
                {
                    error = "Запрашиваемая Вами страница не найдена!"
                });
            }
            else
            {
                SavedLinks = result.Result;
                return Page();
            }
        }
        public async Task<DbModel> GetDbModelAsync(int id)
        {
            try
            {
                var result = await _savedLinks.GetSavedLinks(id);
                return result;
            }
            catch (InvalidOperationException message)
            {
                return null;
            }
        }
        public void OnPost()
        {

        }
        public class UserEditModel
        {
            [StringLength(20)]
            [Display(Name = "Категория", Description = "Укажите новую увткгорию для выбронного ресурса")]
            public string Category{ get; set; }

            [StringLength(50)]
            [Display(Name = "Описание", Description = "Укажите новое описание")]
            public string Description { get; set; }

            [StringLength(250)]
            [Display(Name = "Ссылка на ресурс", Description = "Изменить ссылку на ресурс материала")]
            public string Link { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}
