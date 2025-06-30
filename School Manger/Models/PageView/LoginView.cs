namespace School_Manger.Models.PageView
{
    public class LoginView
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        private string _passwordHash;
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Mobile { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string Status => IsActive ? "فعال" : "غیرفعال";

    }
}
