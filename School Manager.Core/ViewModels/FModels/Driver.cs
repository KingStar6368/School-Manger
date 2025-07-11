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
        /// امتیاز راننده
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// اخطار ها
        /// </summary>
        public int Warnning { get; set; }
        /// <summary>
        /// تعداد صندلی خالی
        /// </summary>
        public int AvailableSeats { get; set; }
        /// <summary>
        /// لیست کد مسافر ها
        /// </summary>
        public List<long> Passanger { get; set; }
        /// <summary>
        /// مشخصات ماشین
        /// </summary>
        public CarInfoDto Car { get; set; }
        /// <summary>
        /// طول جرافیایی
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double? Longitude { get; set; }
        public string? Code { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string BankAccountNumber { get; set; }
    }
    public interface IDriverDto
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public long UserRef { get; set; }
        /// <summary>
        /// شناسه بانک
        /// </summary>
        public long BankRef { get; set; }
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
        public string FatherName { get; set; }
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
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationCode { get; set; }
        /// <summary>
        /// امتیاز راننده
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// اخطار ها
        /// </summary>
        public int Warnning { get; set; }
        /// <summary>
        /// صندلی خالی
        /// </summary>
        public int AvailableSeats { get; set; }
        /// <summary>
        /// طول جرافیایی
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double? Longitude { get; set; }
        /// <summary>
        /// کد پرونده
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string BankAccountNumber { get; set; }
    }
    public class DriverCreateDto : IDriverDto
    {
        public long UserRef {get;set;}
        public long BankRef {get;set;}
        public string Name {get;set;}
        public string LastName {get;set;}
        public string FatherName {get;set;}
        public string CertificateId {get;set;}
        public string Education {get;set;}
        public string Descriptions {get;set;}
        public string Address {get;set;}
        public DateTime BirthDate {get;set;}
        public string NationCode {get;set;}
        public int Rate {get;set;}
        public int Warnning {get;set;}
        public int AvailableSeats { get ; set; }
        /// <summary>
        /// طول جرافیایی
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double? Longitude { get; set; }
        public CarCreateDto CarCreateDto {get;set;}
        public string? Code { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string BankAccountNumber { get; set; }
    }
    public class DriverUpdateDto : IDriverDto
    {
        public long Id { get; set; }
        public long UserRef { get; set; }
        public long BankRef { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string CertificateId { get; set; }
        public string Education { get; set; }
        public string Descriptions { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationCode { get; set; }
        public int Rate { get; set; }
        public int Warnning { get; set; }
        public int AvailableSeats { get ; set ; }
        /// <summary>
        /// طول جرافیایی
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double? Longitude { get; set; }
        public string? Code { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string BankAccountNumber { get; set; }
    }
}
