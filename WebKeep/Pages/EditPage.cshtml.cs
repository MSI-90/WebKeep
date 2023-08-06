using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebKeep.Interfaces;
using WebKeep.Models;

namespace WebKeep.Pages
{
    public class TestPModel : PageModel
    {
        private readonly ISavedLinks _savedLinks;
        public DbModel SavedLinks { get; set; }
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
                //return RedirectToPage("Index1");
            }
            else
            {
                SavedLinks = result.Result;
                return Page();
            }
        }
        public async Task<DbModel> GetDbModelAsync(int id)
        {
            var result = await _savedLinks.GetSavedLinks(id);
            return result;
        }
    }
}
