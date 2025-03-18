using Microsoft.Data.SqlClient;
using Model;
using WebAPI.Database;
namespace WebAPI.Services{
    public class UserService{
        DatabaseCon database;
        public UserService(DatabaseCon database){
            this.database=database;
        }
        //Get User
        public User? GetUser(string userName,string userPassword){
            using(var connection=database.GetConnection()){
                connection.Open();
                var command=new SqlCommand("Articles.GetUser",connection);
                command.CommandType=System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userName",userName);
                command.Parameters.AddWithValue("@userPassword",userPassword);
                using(var reader=command.ExecuteReader()){
                    while(reader.Read()){
                        int userId=Convert.ToInt32(reader["userId"]);
                        string name=Convert.ToString(reader["userName"])??"";
                        string password=Convert.ToString(reader["userPassword"])??"";
                        return new User(userId,userName,userPassword);
                    }
                }
            }
            return null;
        }
    }
}