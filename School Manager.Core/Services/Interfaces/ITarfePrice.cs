using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ITarfePrice
    {
        /// <summary>
        /// گرفتن تعرفه با کد
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TarfePriceDto GetTarfe(int Id);
        /// <summary>
        /// گرفتن تعرفه با نزدیک ترین کیلومتر
        /// </summary>
        /// <param name="Km">کیلومتر ورودی</param>
        /// <returns></returns>
        public TarfePriceDto GetTarfeByKm(float Km);
    }
}
