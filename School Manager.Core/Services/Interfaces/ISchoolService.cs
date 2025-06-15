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
        /// <returns></returns>
        Task<List<ChildInfo>> GetChildren(long id);
        /// <summary>
        /// گرفتن راننده های یک مدرسه
        /// </summary>
        /// <param name="id">کد مدرسه</param>
        /// <returns></returns>
        Task<SchoolDriverDto> GetDrivers(long id);
        
    }
}
