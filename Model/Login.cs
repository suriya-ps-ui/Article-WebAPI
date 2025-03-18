namespace WebAPI.Model{
    public class Login{
        public string userName{get;set;}
        public string userPassword{get;set;}
        public Login(string userName,string userPassword){
            this.userName=userName;
            this.userPassword=userPassword;
        }
    }
}