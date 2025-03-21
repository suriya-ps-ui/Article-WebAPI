using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace WebAPI.Database{
    public class DatabaseCon{
        private Dictionary<string, string> connURL;
        private string sqlConJSON;

        public DatabaseCon(){
            sqlConJSON=File.ReadAllText("Database/SqlCon.json");
            connURL=JsonSerializer.Deserialize<Dictionary<string, string>>(sqlConJSON)??throw new Exception("SqlCon.json is null.");
        }
        public SqlConnection GetConnection(){
            try{
                return new SqlConnection(connURL["ConnectionString"]);
            }
            catch (Exception ex){
                Console.WriteLine($"Error while creating connection with database.\n{ex.Message}");
                throw;
            }
        }
    }
}