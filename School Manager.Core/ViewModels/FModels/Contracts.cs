namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس قرار داد رانندگان
    /// </summary>
    public class DriverContractDto
    {
        /// <summary>
        /// کد کلاس
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کلاس راننده
        /// </summary>
        public DriverDto Driver { get; set; }
        /// <summary>
        /// تاریخ شروع قرار داد 
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// تاریخ اتمام قرار داد
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// کد چک کلاس
        /// </summary>
        public CheckDto Check { get; set; }
        /// <summary>
        /// عکس امضا
        /// </summary>
        public byte[] SignatureImage { get; set; }
    }
    /// <summary>
    /// قرار داد خانواده
    /// </summary>
    public class ServiesContractDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کلاس خانواده
        /// </summary>
        public ParentDto Parent { get; set; }
        /// <summary>
        /// کلاس فرزند
        /// </summary>
        public ChildInfo Child { get; set; }
        /// <summary>
        /// مبلغ کل قرار داد
        /// </summary>
        public long TotalPrice { get; set; }
        /// <summary>
        /// مبلغ در هر ماه
        /// </summary>
        public long MonthPrice { get; set; }
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
        /// کلاس چک
        /// </summary>
        public CheckDto Check { get; set; }
    }
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
        /// مقدار چک
        /// </summary>
        public long Price { get; set; }
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
}
