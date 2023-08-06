using Microsoft.Data.SqlClient;
using System.Data;
using WebKeep.Interfaces;
using WebKeep.Models;

namespace WebKeep.Services
{   
    //Класс, реализующий интерфейс ISavedLinks 

    public class SavedLinksProvider : ISavedLinks
    {
        private readonly IDbConnect _connection;
        public SavedLinksProvider(IDbConnect connection)
        {
            _connection = connection;
        }

        public async Task<List<DbModel>> GetDataAsync()
        {
            List<DbModel> result = new List<DbModel>();
            using(var connection = _connection.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM SavedLinks";
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new DbModel
                        {
                            Id = reader.GetInt32("id"),
                            Categorry = reader.GetString(1),
                            Description = reader.GetString(2),
                            Link = reader.GetString(3),
                            Date = reader.GetString(4),
                        });
                    }
                }
            }
            return result;
        }

        public async Task<DbModel> GetSavedLinks(int id)
        {
            using(var connection = _connection.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * from SavedLinks WHERE Id = @id";
                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "id",
                    Value = id,
                    SqlDbType = SqlDbType.Int
                };
                command.Parameters.Add(parameter);
                using(SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if(reader.HasRows && await reader.ReadAsync())
                    {
                        return new DbModel
                        {
                            Categorry = reader.GetString(1),
                            Description = reader.GetString(2),
                            Link = reader.GetString(3),
                            Date = reader.GetString(4),
                        };
                    }
                }
            }
            return null;
        }
    }
}
