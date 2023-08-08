using Microsoft.Data.SqlClient;
using System.Data;
using WebKeep.Interfaces;
using WebKeep.Models;
using Dapper;

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
            //List<DbModel> result = new List<DbModel>(); for ADO.NET
            using(var connection = _connection.CreateConnection())
            {
                var result = await connection.QueryAsync<DbModel>("SELECT * FROM SavedLinks");
                if (result is null)
                    return new List<DbModel>();
                return result.ToList();
                #region
                // ADO.NET
                //SqlCommand command = connection.CreateCommand();
                //command.CommandText = "SELECT * FROM SavedLinks";
                //using(var reader = await command.ExecuteReaderAsync())
                //{
                //    while (await reader.ReadAsync())
                //    {
                //        result.Add(new DbModel
                //        {
                //            Id = reader.GetInt32("id"),
                //            Categorry = reader.GetString(1),
                //            Description = reader.GetString(2),
                //            Link = reader.GetString(3),
                //            Date = reader.GetString(4),
                //        });
                //    }
                //}
                #endregion

            }
            //return result; for ADO.NET
        }

        public async Task<DbModel> GetSavedLinks(int id)
        {
            using(var connection = _connection.CreateConnection())
            {
                var result = await connection.QueryFirstAsync<DbModel>("SELECT * from SavedLinks WHERE Id = @idparam",
                    new { idparam = id});
                if (result is null)
                    return new DbModel();
                return result;
                #region
                // For ADO.NET
                //SqlCommand command = connection.CreateCommand();
                //command.CommandText = "SELECT * from SavedLinks WHERE Id = @id";
                //SqlParameter parameter = new SqlParameter()
                //{
                //    ParameterName = "id",
                //    Value = id,
                //    SqlDbType = SqlDbType.Int
                //};
                //command.Parameters.Add(parameter);
                //using (SqlDataReader reader = await command.ExecuteReaderAsync())
                //{
                //    if (reader.HasRows && await reader.ReadAsync())
                //    {
                //        return new DbModel
                //        {
                //            Category = reader.GetString(1),
                //            Description = reader.GetString(2),
                //            Link = reader.GetString(3),
                //            Date = reader.GetString(4),
                //        };
                //    }
                //}
                #endregion
            }
            //return null; for ADO.NET
        }
        
    }
}
