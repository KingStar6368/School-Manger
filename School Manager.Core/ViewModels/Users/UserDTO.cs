using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.Users
{
	public class UserDTO
	{
		public int Id { get; set; }
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
        public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public bool IsActive { get; set; }
		public string Status => IsActive ? "فعال" : "غیرفعال";

	}
}
