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
        /// گرفتن یک فرزند
        /// </summary>
        /// <param name="id">کد فرزند</param>
        /// <returns></returns>
        public ChildInfo GetChild(long id);
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
        public SchoolDto GetChildSchool(long ChildId);
        /// <summary>
        /// ایجاد فرزند
        /// </summary>
        /// <param name="child">فرزند</param>
        /// <returns>شناسه فرزند اگر ناموفق صفر می شود</returns>
        public int CreateChild(ChildCreateDto child);
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
    }
}
