using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Channels;
using WebKeep.Interfaces;

namespace WebKeep.Pages
{
    // Проверка соединения с БД, с последующим отображением на странице
    public class IndexModel : PageModel
    {
        public readonly IDbConnect _db;
        public string ConnectionState { get; set; }
        public IndexModel(IDbConnect db)
        {
            _db = db;
        }

        public void OnGet()
        {
            using (var connection = _db.CreateConnection())
            {
                if (ModelState.IsValid)
                {
                    if (_db.ErrorMessage != null)
                        ConnectionState = _db.ErrorMessage;
                    else
                    {
                        ConnectionState = (connection.State.ToString() == "Open") ? "Open" : "Closed";

                        return;
                    }
                }
            }
        }
    }
}