using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Cheque : AuditableEntity<int>
    {
        /// <summary>
        /// شناسه قرارداد
        /// </summary>
        public int SeviceContractRef {  get; set; }
        /// <summary>
        /// مقدار چک
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// سریال چک
        /// </summary>
        public string CheckSerial { get; set; }
        /// <summary>
        /// شناسه صیاد
        /// </summary>
        public string CheckSayadNumber { get; set; }
        /// <summary>
        /// نام بانک
        /// </summary>
        public int BankId { get; set; }
        /// <summary>
        /// نام دارند چک
        /// </summary>
        public string CheckOwner { get; set; }
        /// <summary>
        /// زمان چک
        /// </summary>
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// قرارداد
        /// </summary>
        public ServiceContract ServiceContractNavigation { get; set; }
    }
}
