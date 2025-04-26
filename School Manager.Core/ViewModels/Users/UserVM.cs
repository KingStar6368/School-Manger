using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.Users
{
    public class UserVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public List<FormOpr> formOprs { get; set; }
    }
    public class FormOpr
    {
        public string FormName { get; set; }
        public string OprName { get; set; }
    }
}
