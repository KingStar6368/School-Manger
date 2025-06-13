using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Pay : AuditableEntity<long>
    {
        /// <summary>
        /// مبلغ پرداخت
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// تاریخ پرداخت
        /// </summary>
        public DateTime BecomingTime { get; set; }
        /// <summary>
        /// نحوه پرداخت
        /// </summary>
        public PayType PayType { get; set; }
        /// <summary>
        /// ارتباط با قبض
        /// </summary>
        public virtual ICollection<PayBill> PayBills { get; set; }
    }
}
