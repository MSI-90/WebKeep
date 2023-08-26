using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebKeep.Interfaces;
using WebKeep.Models;

namespace WebKeep.Pages
{
    public class TestPModel : PageModel
    {
        private readonly ISavedLinks _savedLinks;

        //[BindProperty(SupportsGet = true)]
        public DbModel SavedLinks { get; set; }

        [BindProperty]
        public UserEditModel Input { get; set; }
        public List<string> ModelErrors { get; set; }
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
                    error = "������������� ���� �������� �� �������!"
                });
            }
            else
            {
                SavedLinks = result.Result;
                return Page();
            }
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "������ ������");
                return Page();
            }
            else
            {
                if (!string.IsNullOrEmpty(Input.Category) || !string.IsNullOrEmpty(Input.Description) || !string.IsNullOrEmpty(Input.Link))
                {
                    var result = _savedLinks.UpdateSavedLinks(Input, id);
                    if (result.Result != 1)
                    {
                        return RedirectToPage("NotFound", new
                        {
                            error = "���������� �������� ������, �������� ������!"
                        });
                    }
                }
                return RedirectToPage("DataList");
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
        public IActionResult OnGetDeleteSavedlinks(int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "������ ������");
                return Page();
            }
            else
            {
                var result = _savedLinks.DeleteSavedLinks(id);
                if (result.Result != 1)
                {
                    return RedirectToPage("NotFound", new
                    {
                        error = "���������� ������� ������, �������� ������!"
                    });
                }
                return RedirectToPage("DataList");
            }
        }
        public class UserEditModel
        {
            [MinLength(0)]
            [StringLength(20)]
            [Display(Name = "���������")]
            public string? Category{ get; set; }

            [MinLength(0)]
            [StringLength(200)]
            [Display(Name = "��������")]
            public string? Description { get; set; }

            [MinLength(0)]
            [StringLength(500)]
            [Display(Name = "������ �� ������")]
            public string? Link { get; set; }
        }
    }
}
