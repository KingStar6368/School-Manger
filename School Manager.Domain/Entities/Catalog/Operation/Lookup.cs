using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Common;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Lookup : AuditableEntity<int>
    {
        public string Type { get; set; }
        public int Code { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
    }
}
