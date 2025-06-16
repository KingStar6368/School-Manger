using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ISchoolService
    {
        /// <summary>
        /// لیست مدرسه ها
        /// </summary>
        /// <returns></returns>
        Task<List<SchoolDto>> GetSchools();
        /// <summary>
        /// گرفتن مدرسه
        /// </summary>
        /// <param name="id">کد مدرسه</param>
        /// <returns></returns>
        SchoolDto GetSchool(long id);
        /// <summary>
        /// گرفتن لیست دانش آموزان یک مدرسه
        /// </summary>
        /// <param name="id">کد مدرسه</param>
        /// <returns>لیست دانش آموزانی که کد انها در کلاس مدرسه است</returns>
        Task<List<ChildInfo>> GetChildren(long id);
        /// <summary>
        /// گرفتن راننده های یک مدرسه در کلاس مدرسه driver id
        /// </summary>
        /// <param name="id">کد مدرسه</param>
        /// <returns>لیست راننده های که در کلاس مدرسه کد ان وجود دارد</returns>
        Task<List<DriverDto>> GetDrivers(long id);
        /// <summary>
        /// ایجاد
        /// </summary>
        /// <param name="school">مدرسه</param>
        /// <returns>شناسه مدرسه ثبت شده</returns>
        long CreateSchool(SchoolCreateDto school);
        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="id">شناسه مدرسه</param>
        /// <returns>آیا موفق بود؟</returns>
        bool DeleteSchool(long id);
        /// <summary>
        /// بروز رسانی
        /// </summary>
        /// <param name="school">مدرسه</param>
        /// <returns>آیا موفقیت آمیز بود؟</returns>
        bool UpdateSchool(SchoolUpdateDto school);
    }
}
