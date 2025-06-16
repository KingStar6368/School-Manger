using School_Manager.Domain.Entities.Catalog.Enums;
namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس راننده
    /// </summary>
    public class DriverDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// نام پدر
        /// </summary>
        public string FutherName { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate {  get; set; }
        /// <summary>
        /// شماره گواهی
        /// </summary>
        public string CertificateId { get; set; }
        /// <summary>
        /// تحصیلات
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// مشحصات
        /// </summary>
        public string Descriptions { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// نام بانک
        /// </summary>
        public LookupComboViewModel BankAccount { get; set; }
        /// <summary>
        /// شماره بانک
        /// </summary>
        public string BankNumber { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationCode { get; set; }
        /// <summary>
        /// امتیاز راننده NewV
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// اخطار ها NewV
        /// </summary>
        public int Warnning { get; set; }
        /// <summary>
        /// لیست کد مسافر ها NewC
        /// </summary>
        public List<long> Passanger { get; set; }
        /// <summary>
        /// مشخصات ماشین
        /// </summary>
        public CarInfoDto Car { get; set; }
    }
}
