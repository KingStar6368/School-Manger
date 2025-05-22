using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Core.ViewModels.FModels
{
    public class LoginUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserType Type { get; set; }
    }
}
