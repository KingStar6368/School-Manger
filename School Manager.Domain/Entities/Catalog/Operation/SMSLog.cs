using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class SMSLog : AuditableEntity<long>
    {
        public long UserId { get; set; }
        public SMSType type { get; set; }
        public DateTime SMSTime { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
