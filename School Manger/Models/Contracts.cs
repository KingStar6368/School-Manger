namespace School_Manger.Models
{
    /// <summary>
    /// کلاس قرار داد رانندگان
    /// </summary>
    public class DriverContract
    {
        /// <summary>
        /// کد کلاس
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کلاس راننده
        /// </summary>
        public Driver Driver { get; set; }
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
        public long CheckId { get; set; }
        /// <summary>
        /// عکس امضا
        /// </summary>
        public byte[] SignatureImage { get; set; }
    }
    /// <summary>
    /// قرار داد خانواده
    /// </summary>
    public class ParentContract
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کلاس خانواده
        /// </summary>
        public Parent Parent { get; set; }
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
    }
    public class ServiesContract
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public long ChildId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class Check
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public long Price { get; set; }
        public string BankName { get; set; }
        public string CheckOwner { get; set; }
        public DateTime CheckTime { get; set; }
    }
}
