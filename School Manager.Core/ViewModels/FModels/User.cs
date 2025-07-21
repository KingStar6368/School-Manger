using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        private string _passwordHash;
        public string Password
        {
            get => Utilities.Values.EntityHelper.Decrypt(_passwordHash);
            set => _passwordHash = Utilities.Values.EntityHelper.Encrypt(value);
        }
        public string PasswordHash
        {
            get => _passwordHash;
            set => _passwordHash = value;
        }
        public string FirstName { get; set; }
        public string Mobile { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get => FirstName + " " + LastName;
        }
        public bool IsActive { get; set; }
        public string Status => IsActive ? "فعال" : "غیرفعال";

    }

    public class UserCreateDTO
    {
        public  string UserName { get; set; }
        public string PasswordHash { get; set; }
        public  string Mobile { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public bool IsActive { get; set; }
    }
    public class UserUpdateDTO
    {
        public long Id { get; set; }
        public  string UserName { get; set; }
        public string PasswordHash{ get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}
