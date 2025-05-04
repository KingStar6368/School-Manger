namespace School_Manger.Models
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserType Type { get; set; }
    }
    public enum UserType
    {
        Parent,
        Driver,
        Admin
    }
}
