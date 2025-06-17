using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس ماشین
    /// </summary>
    public class CarInfoDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام ماشین
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// پلاک ماشین
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// رنگ
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// صندلی خالی
        /// </summary>
        public int AvailableSeats { get; set; }
        /// <summary>
        /// تعداد کل صندلی
        /// </summary>
        public int SeatNumber { get; set; }
        /// <summary>
        /// نوع ماشین
        /// </summary>
        public CarType carType { get; set; }
    }
    public interface ICarDto
    {
        /// <summary>
        /// شناسه راننده
        /// </summary>
        public long DriverRef { get; set; }
        /// <summary>
        /// نام ماشین
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// دو رقم اول
        /// </summary>
        public int FirstIntPlateNumber { get; set; }
        /// <summary>
        /// حرف وسط
        /// </summary>
        public string ChrPlateNumber { get; set; }
        /// <summary>
        /// سه رقم بعد از حرف
        /// </summary>
        public int SecondIntPlateNumber { get; set; }
        /// <summary>
        /// کد شهر
        /// </summary>
        public int ThirdIntPlateNumber { get; set; }
        /// <summary>
        /// رنگ
        /// </summary>
        public int ColorCode { get; set; }
        /// <summary>
        /// صندلی خالی
        /// </summary>
        public int AvailableSeats { get; set; }
        /// <summary>
        /// فعال
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// تعداد کل صندلی
        /// </summary>
        public int SeatNumber { get; set; }
        /// <summary>
        /// نوع ماشین
        /// </summary>
        public int carType { get; set; }
    }
    public class CarCreateDto : ICarDto
    {
        public long DriverRef {get;set;}
        public string Name {get;set;}
        public int FirstIntPlateNumber {get;set;}
        public string ChrPlateNumber {get;set;}
        public int SecondIntPlateNumber {get;set;}
        public int ThirdIntPlateNumber {get;set;}
        public int ColorCode {get;set;}
        public int AvailableSeats {get;set;}
        public bool IsActive {get;set;}
        public int SeatNumber {get;set;}
        public int carType {get;set;}
    }
    public class CarUpdateDto : ICarDto
    {
        public long Id {get;set;}
        public long DriverRef { get; set; }
        public string Name { get; set; }
        public int FirstIntPlateNumber { get; set; }
        public string ChrPlateNumber { get; set; }
        public int SecondIntPlateNumber { get; set; }
        public int ThirdIntPlateNumber { get; set; }
        public int ColorCode { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsActive { get; set; }
        public int SeatNumber { get; set; }
        public int carType { get; set; }
    }
}
