using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس چک
    /// </summary>
    public class CheckDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کد قرار داد
        /// </summary>
        public long ContractId { get; set; }
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
        public LookupComboViewModel BankName { get; set; }
        /// <summary>
        /// نام دارند چک
        /// </summary>
        public string CheckOwner { get; set; }
        /// <summary>
        /// زمان چک
        /// </summary>
        public DateTime CheckTime { get; set; }
    }
    public interface IChequeDto
    {
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
    }
    public class ChequeCreateDto : IChequeDto
    {
        public long Price { get; set; }
        public string CheckSerial { get; set; }
        public string CheckSayadNumber { get; set; }
        public int BankId { get; set; }
        public string CheckOwner { get; set; }
        public DateTime CheckTime { get; set; }
    }
    public class ChequeUpdateDto : IChequeDto
    {
        public long Id { get; set; }
        public long Price { get; set; }
        public string CheckSerial { get; set; }
        public string CheckSayadNumber { get; set; }
        public int BankId { get; set; }
        public string CheckOwner { get; set; }
        public DateTime CheckTime { get; set; }
    }

    public class DriverContractChequeUpdateDto
    {
        public long Id { get; set; }
        public long DriverContractRef { get; set; }
        public long ChequeRef { get; set; }
    }
    public class ServiceContractChequeUpdateDto
    {
        public long Id { get; set; }
        public long ServiceContractRef { get; set; }
        public long ChequeRef { get; set; }
    }
}
