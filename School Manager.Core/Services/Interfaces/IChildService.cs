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
        /// <param name="DriverId">کد راننده</param>
        /// <returns></returns>
        public DriverDto GetChildDriver(long DriverId);
        /// <summary>
        /// گرفتن مدرسه فرزند
        /// </summary>
        /// <param name="SchoolId">کد مدرسه</param>
        /// <returns></returns>
        public School GetChildSchool(long SchoolId);
        /// <summary>
        /// گرفتن لیست قبض های یک دانش آموز
        /// </summary>
        /// <param name="id">کد دانش آموز</param>
        /// <returns></returns>
        public List<BillDto> GetChildBill(long id);
        /// <summary>
        /// گرفتن یک قبض
        /// </summary>
        /// <param name="id">کد قبض</param>
        /// <returns></returns>
        public BillDto GetBill(long id);
    }
}
