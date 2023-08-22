using System.ComponentModel.DataAnnotations.Schema;

namespace WebKeep.Models
{
    public class DbModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string Date { get; set; } = DateTime.UtcNow.ToString("dd.mm.yyyy");
        public int IndexCount { get; set; }
    }
}
