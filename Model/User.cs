namespace Model{
    public class User{
        public int userId{get;set;}
        public string userName{get;set;}
        public string userPassword{get;set;}
        public User(int userId,string userName,string userPassword){
            this.userId=userId;
            this.userName=userName;
            this.userPassword=userPassword;
        }
    }
}