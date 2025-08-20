using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manager.Domain.Interfaces;

namespace School_Manager.Domain.Entities.Catalog.Identity
{
    public  class User : AuditableEntity<long>
    {
        public  string UserName { get; set; }
        public  string PasswordHash { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public UserType Type { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<SMSLog> SMSLogs { get; set; }
    }
}
