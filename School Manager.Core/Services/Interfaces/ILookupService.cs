using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ILookupService
    {
        /// <summary>
        /// گرفتن یک سطر lookUp
        /// </summary>
        /// <param name="Type">نوع</param>
        /// <param name="Code">شناسه سطر</param>
        /// <returns></returns>
        LookupComboViewModel? GetLookUp(string Type,int Code);
        /// <summary>
        /// دریافت اطلاعات برای Combo
        /// </summary>
        /// <param name="Type">نوع اطلاعات مثال:بانکها</param>
        /// <returns></returns>
        Task<List<LookupComboViewModel>> GetLookupTypesAsync(string Type);
    }
}
