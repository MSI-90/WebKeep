using Microsoft.Data.SqlClient;
using WebKeep.Models;

namespace WebKeep.Interfaces
{
    //интерфейс, ответственный за соединение с БД

    public interface IDbConnect
    {
        string? ErrorMessage { get; set; }
        SqlConnection? CreateConnection();
    }
}
