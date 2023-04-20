namespace MyTasks.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string kind { get; set; }

        public User(int UserID, string UserName, string Password, string kind)
        {
            this.UserId = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.kind = kind;
        }
    }
}