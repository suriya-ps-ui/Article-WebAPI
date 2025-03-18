using Microsoft.Data.SqlClient;
using WebAPI.Database;
using WebAPI.Model;

namespace WebAPI.Services{
    public class ArticleService{
        private readonly DatabaseCon database;

        public ArticleService(DatabaseCon database){
            this.database=database??new DatabaseCon();
        }

        // Get
        public List<Article> GetArticleRecords(){
            List<Article> articles=new List<Article>();
            using(var connection=database.GetConnection()){
                connection.Open();
                var command=new SqlCommand("Articles.GetValues", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using(var reader=command.ExecuteReader()){
                    while (reader.Read()){
                        int articleId=Convert.ToInt32(reader["articleId"]);
                        string articleTitle=Convert.ToString(reader["articleTitle"])??"";
                        articles.Add(new Article(articleId, articleTitle));
                    }
                }
                connection.Close();
            }
            return articles;
        }
        // Post
        public void PostArticleRecords(Article article){
            using(var connection=database.GetConnection()){
                connection.Open();
                var command=new SqlCommand("Articles.PostRecord",connection);
                command.CommandType=System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@articleID",article.articleId);
                command.Parameters.AddWithValue("@articleTitle",article.articleTitle);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        // Delete
        public void DeleteArticleRecords(int articleId){
            using(var connection=database.GetConnection()){
                connection.Open();
                var command=new SqlCommand("Articles.DeleteRecord", connection);
                command.CommandType=System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@articleID",articleId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        // Put
        public void UpdateRecords(int articleId, string articleTitle){
            using(var connection=database.GetConnection()){
                connection.Open();
                var command=new SqlCommand("Articles.UpdateRecord", connection);
                command.CommandType=System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@articleId",articleId);
                command.Parameters.AddWithValue("@articleTitle",articleTitle);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Post & Put
        public void PostOrUpdateRecords(Article article){
            using(var connection=database.GetConnection()){
                connection.Open();
                var command=new SqlCommand("Articles.PostOrUpdateRecord",connection);
                command.CommandType=System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@articleId",article.articleId);
                command.Parameters.AddWithValue("@articleTitle",article.articleTitle);
                command.ExecuteNonQuery();
            }
        }
    }
}