using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class PayBill
    {
        /// <summary>
        /// شناسه پرداخت
        /// </summary>
        public int PayRef { get; set; }
        /// <summary>
        /// شناسه قبض
        /// </summary>
        public int BillRef { get; set; }
        public Pay PayNavigation { get; set; }
        public Bill BillNavigation { get; set; }
    }
}
