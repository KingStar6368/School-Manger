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
    public interface IDriverContractDto
    {
        /// <summary>
        /// شناسه راننده
        /// </summary>
        public long DriverRef { get; set; }
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
    public class DriverContractCreateDto : IDriverContractDto
    {
        public long DriverRef { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class DriverContractUpdateDto : IDriverContractDto
    {
        public long Id { get; set; }
        public long DriverRef { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }

    /// <summary>
    /// قرار داد خانواده
    /// </summary>
    public class ServiceContractDto
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
    public interface IServiceContractDto
    {
        /// <summary>
        /// شناسه فرزند
        /// </summary>
        public long ChildRef { get; set; }
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
    public class ServiceContractCreateDto : IServiceContractDto
    {
        public long ChildRef { get; set; }
        public long TotalPrice { get; set; }
        public long MonthPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class ServiceContractUpdateDto : IServiceContractDto
    {
        public long Id { get; set; }
        public long ChildRef { get; set; }
        public long TotalPrice { get; set; }
        public long MonthPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
}
