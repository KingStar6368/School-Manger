using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class DriverContractCheque : AuditableEntity<long>
    {
        public long DriverContractRef { get; set; }
        public long ChequeRef { get; set; }
        public virtual DriverContract DriverContractNavigation { get; set; }
        public virtual Cheque ChequeNavigation { get; set; }
    }
}
