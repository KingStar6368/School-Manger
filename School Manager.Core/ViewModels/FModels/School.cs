using System.ComponentModel.DataAnnotations;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس مدرسه
    /// </summary>
    public class SchoolDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام مدرسه
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// نام مدیر
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// آدرس مدرسه
        /// </summary>
        public LocationDataDto Address { get; set; } = new LocationDataDto();
        /// <summary>
        /// لیست از رانندگان این مدرسه 
        /// </summary>
        public List<long> DriverIds { get; set; }
        /// <summary>
        /// کد دانش آموزان
        /// </summary>
        public List<long> StudentIds { get; set; }
    }
    public interface ISchoolDto
    {
        public string Name { get; set; }
        /// <summary>
        /// نام مدیر
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public int Rate { get; set; } // 0-5
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double Longitude { get; set; }
    }
    public class SchoolCreateDto : ISchoolDto
    {
        public string Name {get;set;}
        public string ManagerName {get;set;}
        public int Rate {get;set;}
        public string Address {get;set;}
        public double Latitude {get;set;}
        public double Longitude {get;set;}
    }
    public class SchoolUpdateDto : ISchoolDto
    {
        public long Id { get; set; }
        public string Name {get;set;}
        public string ManagerName {get;set;}
        public int Rate {get;set;}
        public string Address {get;set;}
        public double Latitude {get;set;}
        public double Longitude {get;set;}
    }
}
