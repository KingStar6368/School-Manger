using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IChildService
    {
        /// <summary>
        /// گرفتن تمامی فرزند ها 
        /// </summary>
        /// <returns></returns>
        public Task<List<ChildInfo>> GetChildren();
        /// <summary>
        /// سرچ کردن دانش آموزان ها با داده های ورودی
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public Task<SearchChildDto> SearchChild(SearchDto filter);
        /// <summary>
        /// لیست دانش آموزان پرداخت نشده
        /// </summary>
        /// <returns></returns>
        public Task<List<ChildInfo>> GetNonPaidChildren();
        /// <summary>
        /// گرفتن یک فرزند
        /// </summary>
        /// <param name="id">کد فرزند</param>
        /// <returns></returns>
        public ChildInfo GetChild(long id);
        /// <summary>
        /// گرفتن فرزند یک والد با جزئیات
        /// </summary>
        /// <param name="ParentId">کد والد</param>
        /// <returns></returns>
        public List<ChildInfo> GetChildrenParent(long ParentId);
        /// <summary>
        /// گرفتن فرزند با کد ملی
        /// </summary>
        /// <param name="nationCode"></param>
        /// <returns></returns>
        public ChildInfo GetChildByNationCode(string nationCode);
        /// <summary>
        /// گرفتن راننده فرزند
        /// </summary>
        /// <param name="DriverId">کد فرزند</param>
        /// <returns></returns>
        public DriverDto GetChildDriver(long ChildId);
        /// <summary>
        /// گرفتن مدرسه فرزند
        /// </summary>
        /// <param name="ChildId">کد دانش آموز</param>
        /// <returns></returns>
        public long GetChildSchool(long ChildId);
        /// <summary>
        /// مشخص کردن رانند با کد رانند
        /// در راننده باید فیلد passanger اضافه شود
        /// در child باید driverid ست شود
        /// </summary>
        /// <param name="ChildId"></param>
        /// <param name="DriverId"></param>
        public bool SetDriver(long ChildId, long DriverId); 
        /// <summary>
        /// حذف ارتباط راننده و دانش آموز
        /// </summary>
        /// <param name="ChildId"></param>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public bool RemoveDriverFromChild(long ChildId, long DriverId = 0);
        /// <summary>
        /// مشخص کردن مدرسه دانش آموز با کد مدرسه
        /// در فرزند باید school id تغییر پیدا کند
        /// </summary>
        /// <param name="ChildId"></param>
        /// <param name="SchoolId"></param>
        public bool SetSchool(long ChildId, long SchoolId);
        /// <summary>
        /// ایجاد فرزند
        /// </summary>
        /// <param name="child">فرزند</param>
        /// <returns>شناسه فرزند اگر ناموفق صفر می شود</returns>
        public long CreateChild(ChildCreateDto child);
        /// <summary>
        /// بروز رسانی فرزند
        /// </summary>
        /// <param name="child">فرزند</param>
        /// <returns>عملیات موفقیت آمیز بود؟</returns>
        public bool UpdateChild(ChildUpdateDto child);
        /// <summary>
        /// حذف فرزند
        /// </summary>
        /// <param name="ChildId">شناسه فرزند</param>
        /// <returns>عملیات موفقیت آمیز بود؟</returns>
        public bool DeleteChild(long ChildId);
        /// <summary>
        /// دریافت دانش آموزان بدون راننده
        /// </summary>
        /// <param name="DriverId">آی دی راننده در صورت نیاز اگر انتخاب شود دانش آموزان محدوده راننده فیلتر می شود</param>
        /// <param name="SchoolId">آی دی مدرسه در صورت نیاز اگر انتخاب شود دانش آموزان مدرسه می آید اگر راننده نداری باید صفر وارد شود</param>
        /// <param name="radiusInMeters">محدوده به متر پیش فرض 500</param>
        /// <returns>لیست دانش آموزان</returns>
        Task<List<ChildInfo>> GetChildWithoutDriver(long DriverId = 0, long SchoolId = 0, double radiusInMeters = 500);
        /// <summary>
        /// لیست رانندگان بدون دانش آموز
        /// </summary>
        /// <returns>لست رانندگان</returns>
        Task<List<DriverDto>> GetDriverFree(long shiftId);
    }
}
