using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manager.Domain.Interfaces;

namespace School_Manager.Domain.Entities.Catalog.Identity
{
    public  class User : AuditableEntity<int>
    {
        public required string UserName { get; set; }
        public  string PasswordHash { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
