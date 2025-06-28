using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IDriverService
    {
        /// <summary>
        /// لیست راننده ها 
        /// </summary>
        /// <returns></returns>
        Task<List<DriverDto>> GetDrivers();
        /// <summary>
        /// گرفتن یک راننده با کد
        /// </summary>
        /// <param name="Id">کد راننده</param>
        /// <returns></returns>
        DriverDto GetDriver(long Id);
        /// <summary>
        /// گرفتن راننده با کد ملی
        /// </summary>
        /// <param name="NationCode"></param>
        /// <returns></returns>
        DriverDto GetDriverNationCode(string NationCode);
        /// <summary>
        /// گرفتن لیست دانش آموزان راننده با کد راننده
        /// </summary>
        /// <param name="id">کد راننده</param>
        /// <returns>کلاس دانش آموزانی که در لیست کد مسافر راننده وجود دارد</returns>
        List<ChildInfo> GetPassngers(long id);
        /// <summary>
        /// گرفتن راننده های فعال در مدرسه
        /// </summary>
        /// <param name="SchoolId">شناسه مدرسه</param>
        /// <returns>کلاس راننده های که در لیست رانندگان مدرسه وجود دارد</returns>
        Task<List<DriverDto>> GetDrivers(long SchoolId);
        /// <summary>
        /// ایجاد راننده
        /// </summary>
        /// <param name="driver">راننده</param>
        /// <returns>شناسه راننده</returns>
        long CreateDriver(DriverCreateDto driver);
        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="id">شناسه راننده</param>
        /// <returns>آیا موفق بود؟</returns>
        bool DeleteDriver(long id);
        /// <summary>
        /// بروز رسانی
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        bool UpdateDriver(DriverUpdateDto driver);
    }
}
