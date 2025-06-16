namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس قبض
    /// </summary>
    public class BillDto
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
        /// مبلغ کل قبض
        /// </summary>
        public long TotalPrice { get; set; }    
        /// <summary>
        /// مبلغ پرداخت شده
        /// </summary>
        public long PaidPrice { get; set; }
        /// <summary>
        /// پرداخت شده؟
        /// </summary>
        public bool HasPaid {  get; set; }
        /// <summary>
        /// زمان پرداخت کامل
        /// </summary>
        public DateTime? PaidTime { get; set; }
    }
    public class BillCreateDto
    {
        /// <summary>
        /// کد قرار داد
        /// </summary>
        public long ServiceContractRef { get; set; }
        /// <summary>
        /// مبلغ کل قبض
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// مهلت پرداخت
        /// </summary>
        public DateTime EstimateTime { get; set; }
    }
    public class BillUpdateDto
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کد قرار داد
        /// </summary>
        public long ServiceContractRef { get; set; }
        /// <summary>
        /// مبلغ کل قبض
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// مهلت پرداخت
        /// </summary>
        public DateTime EstimateTime { get; set; }
    }
}
