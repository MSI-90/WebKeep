using Microsoft.Data.SqlClient;
using System.Data;
using WebKeep.Interfaces;
using WebKeep.Models;
using Dapper;
using static WebKeep.Pages.TestPModel;
using NuGet.Protocol.Plugins;
using WebKeep.Pages;
using static WebKeep.Pages.Index2Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.WebRequestMethods;
using static WebKeep.Pages.DataListModel;

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
        public async Task<IEnumerable<DbModel>> GetDataAsync()
        {
            using(var connection = _connection.CreateConnection())
            {
                var result = await connection.QueryAsync<DbModel>("SELECT * FROM SavedLinks ORDER BY Category");
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
        public async Task<int> AddNewItemInSavedLinks(UserInputModel model)
        {
            using(var connection = _connection.CreateConnection())
            {

                var query = $"INSERT INTO SavedLinks (Category, Description, Link, Date) VALUES ('{model.Category}', '{model.Description}', " +
                    $"'{model.Link}', '{model.Date}')";
                var result = await connection.ExecuteAsync(query);
                return result;
            }
        }

        // Метод с помощью которого происходит извлечение набора записей из поля Category
        // Затем выбранные записи помещаются в SelectListItem для отображения на странице
        public async Task<SelectListItem[]> GetCategory()
        {
            using( var connection = _connection.CreateConnection())
            {
                var query = "SELECT DISTINCT Category FROM SavedLinks";
                var result = await connection.QueryAsync<string>(query);
                string[] resultList = result.ToArray();
                
                SelectListItem[] categoryResult = new SelectListItem[result.Count()];
                for (int i = 0; i < result.Count(); i++)
                {
                    categoryResult[i] = new SelectListItem()
                    {
                        Text = resultList[i],
                        Value = resultList[i]
                    };
                }
                if (categoryResult != null)
                    return categoryResult;
                else
                    return Array.Empty<SelectListItem>();
            }
        }
        public async Task<IEnumerable<DbModel>> GetAllDataFromCategory(FilterSortModel sort)
        {

            using (var connection = _connection.CreateConnection())
            {
                var query = $"SELECT * FROM SavedLinks WHERE Category='{sort.Categories}'";
                var result = await connection.QueryAsync<DbModel>(query);
                if (result is null)
                    return new List<DbModel>();
                return result.ToList();
            }
        }

    }
}
