using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class PayBill : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه پرداخت
        /// </summary>
        public long PayRef { get; set; }
        /// <summary>
        /// شناسه قبض
        /// </summary>
        public long BillRef { get; set; }
        /// <summary>
        /// پرداخت
        /// </summary>
        public Pay PayNavigation { get; set; }
        /// <summary>
        /// قبض
        /// </summary>
        public Bill BillNavigation { get; set; }
    }
}
