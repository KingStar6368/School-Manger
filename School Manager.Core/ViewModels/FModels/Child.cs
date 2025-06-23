using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس فرزند
    /// </summary>
    public class ChildInfo
    {
        /// <summary>
        /// کد
        /// </summary>
         public long Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کلاس تحصیلی
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// کد راننده 
        /// </summary>
        public long? DriverId { get; set; }
        /// <summary>
        /// کد مدرسه 
        /// </summary>
        public long? SchoolId { get; set; }
        /// <summary>
        /// کامل پرداخت شده ؟
        /// </summary>
        public bool HasPaid { get; set; }
        /// <summary>
        /// مسیر خانه تا مدرسه
        /// </summary>
        public LocationPairModel Path { get; set; }
        /// <summary>
        /// لیست قبص ها
        /// </summary>
        public List<BillDto> Bills { get; set; }
    }
    public interface IChildDto
    {
        /// <summary>
        /// شناسه والدین
        /// </summary>
        public long ParentRef { get; set; }
        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        public long? SchoolRef { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کلاس تحصیلی
        /// </summary>
        public int Class { get; set; }
        public List<LocationPairCreateDto> LocationPairs { get; set; }
    }
    public class ChildCreateDto : IChildDto
    {
        /// <summary>
        /// شناسه والدین
        /// </summary>
        public long ParentRef { get; set; }
        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        public long? SchoolRef { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کلاس تحصیلی
        /// </summary>
        public int Class { get; set; }
        public List<LocationPairCreateDto> LocationPairs { get; set; } = new();
    }
    public class ChildUpdateDto : IChildDto
    {
        public long Id { get; set; }
        /// <summary>
        /// شناسه والدین
        /// </summary>
        public long ParentRef { get; set; }
        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        public long? SchoolRef { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کلاس تحصیلی
        /// </summary>
        public int Class { get; set; }
        public List<LocationPairCreateDto> LocationPairs { get; set; } = new();
    }
}
