using Microsoft.Data.SqlClient;
using System.Data;
using WebKeep.Interfaces;
using WebKeep.Models;
using Dapper;
using static WebKeep.Pages.TestPModel;
using NuGet.Protocol.Plugins;
using WebKeep.Pages;

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
            using(var connection = _connection.CreateConnection())
            {
                var result = await connection.QueryAsync<DbModel>("SELECT * FROM SavedLinks");
                if (result is null)
                    return new List<DbModel>();
                return result.ToList();
            }
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
            }
        }
        public async Task<int> UpdateSavedLinks(UserEditModel model, int id)
        {
            var result = 0;
            using (var connection = _connection.CreateConnection())
            {
                if (!string.IsNullOrEmpty(model.Category))
                {
                    var query = $"UPDATE SavedLinks SET Category = @Category WHERE Id = {id}";
                    result = await connection.ExecuteAsync(query, model);
                }
                if (!string.IsNullOrEmpty(model.Description))
                {
                    var query = $"UPDATE SavedLinks SET Description = @Description WHERE Id = {id}";
                    result = await connection.ExecuteAsync(query, model);
                }
                if (!string.IsNullOrEmpty(model.Link))
                {
                    var query = $"UPDATE SavedLinks SET Link = @Link WHERE Id = {id}";
                    result = await connection.ExecuteAsync(query, model);
                }
            }
            return result;
        }
        public async Task<int> DeleteSavedLinks(int id)
        {
            var result = 0;
            using (var connection = _connection.CreateConnection())
            {
                var inSavedLinks = GetSavedLinks(id);
                if (inSavedLinks.Result != null)
                {
                    var query = $"DELETE FROM SavedLinks WHERE id = {id}";
                    result = await connection.ExecuteAsync(query);
                }
            }
            return result;
        }
    }
}
