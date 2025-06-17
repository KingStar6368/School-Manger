using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ICarService
    {
        /// <summary>
        /// ایجاد
        /// </summary>
        /// <param name="car">ماشین</param>
        /// <returns>شناسه ماشین</returns>
        long CreateCar(CarCreateDto car);
        /// <summary>
        /// بروز رسانی
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        bool UpdateCar(CarUpdateDto car);
        bool DeleteCar(long carId);
    }
}
