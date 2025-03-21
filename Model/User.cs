using System.ComponentModel.DataAnnotations;
namespace Model{
    public class User{
        public int userId{get;set;}
        [MaxLength(50)]
        public string userName{get;set;}
        [MaxLength(50)]
        public string userPassword{get;set;}
        public User(int userId,string userName,string userPassword){
            this.userId=userId;
            this.userName=userName;
            this.userPassword=userPassword;
        }
    }
}