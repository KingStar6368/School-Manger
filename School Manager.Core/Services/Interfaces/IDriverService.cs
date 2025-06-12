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
        /// <returns></returns>
        List<ChildInfo> GetPassngers(long id);
    }
}
