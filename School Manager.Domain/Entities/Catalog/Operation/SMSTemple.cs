using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class SMSTemple : AuditableEntity<long>
    {
        public string Name { get; set; }
        public long TempleId { get; set; }
    }
}
