using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static WebKeep.Pages.Index1Model;

namespace WebKeep.Pages
{
    public class Index1Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }
        public void OnGet()
        {
        }
        public class InputModel
        {
            public string Error { get; set; }
        }
    }
}
