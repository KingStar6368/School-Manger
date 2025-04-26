using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.Users
{
	public class UserCreateDTO
	{
		public required string UserName { get; set; }
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
        public required string Email { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public bool IsActive { get; set; }
	}
}
