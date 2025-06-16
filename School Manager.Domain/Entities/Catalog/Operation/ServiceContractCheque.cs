using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class ServiceContractCheque : AuditableEntity<long>
    {
        public long ServiceContractRef { get; set; }
        public long ChequeRef { get; set; }
        public virtual ServiceContract ServiceContractNavigation { get; set; }
        public virtual Cheque ChequeNavigation { get; set; }
    }
}
