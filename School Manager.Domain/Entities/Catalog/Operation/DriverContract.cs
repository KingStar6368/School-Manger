using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class DriverContract : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه راننده
        /// </summary>
        public int DriverRef { get; set; }
        /// <summary>
        /// شناسه چک
        /// </summary>
        public int ChequeRef { get; set; }
        /// <summary>
        /// تاریخ شروع قرار داد 
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// تاریخ اتمام قرار داد
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// عکس امضا
        /// </summary>
        public byte[] SignatureImage { get; set; }
        /// <summary>
        /// کد چک کلاس
        /// </summary>
        public Cheque ChequeNavigation { get; set; }
        /// <summary>
        /// کلاس راننده
        /// </summary>
        public Driver DriverNavigation { get; set; }
    }
}
