using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model{
    public class Article{
        public  int articleId{get;set;}
        [MaxLength(250)]
        public  string articleTitle{get;set;}
        public Article(int articleId,string articleTitle){
            this.articleId=articleId;
            this.articleTitle=articleTitle;
        }
    }
}