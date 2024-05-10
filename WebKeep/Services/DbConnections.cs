using Microsoft.Data.SqlClient;
using WebKeep.Interfaces;


namespace WebKeep.Services
{
    //класс, реализующий интерфейс IDbConnection

    public class DbConnections: IDbConnect
    {
        private readonly IConfiguration _configuration;
        public string? ErrorMessage { get; set; } 
        public DbConnections(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection? CreateConnection()
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultString")))
                {
                    connection.Open();
                    return connection;
                }
                
                //if (connection is null)
                //{
                //    ErrorMessage = "Отсутствует база данных";
                //    return null;
                //} 
                //connection?.Open();
                //return connection;
                    
            }
            catch(SqlException)
            {
                ErrorMessage = "Отсутсвует база данных";
                return null;
            }
            //catch (InvalidOperationException message)
            //{
            //    ErrorMessage = message.Message.ToString();
            //    return null;
            //}
            //catch (SqlException message)
            //{
            //    ErrorMessage = message.Message.ToString();
            //    return null;
            //}
            
        }
    }
}
