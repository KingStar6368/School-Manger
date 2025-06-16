using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IParentService
    {
        /// <summary>
        /// لیست والدین
        /// </summary>
        /// <returns></returns>
        Task<List<ParentDto>> GetParents();
        /// <summary>
        /// گرفتن یک والد
        /// </summary>
        /// <param name="Id">کد والد</param>
        /// <returns></returns>
        ParentDto GetParent(long Id);
        /// <summary>
        /// گرفتن والد با کد ملی
        /// </summary>
        /// <param name="NationCode">کد ملی</param>
        /// <returns></returns>
        ParentDto GetParentByNationCode(string NationCode);
        /// <summary>
        /// گرفتن والد با شماره تلفن
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        ParentDto GetParentByPhone(string Phone);
        /// <summary>
        /// ایجاد
        /// </summary>
        /// <param name="parent">والد</param>
        /// <returns>شناسه والد ثبتی</returns>
        long CreateParent(ParentCreateDto parent);
        /// <summary>
        /// بروز رسانی
        /// </summary>
        /// <param name="parent">والد</param>
        /// <returns>آیا موفق بود؟</returns>
        bool UpdateParent(ParentUpdateDto parent);
        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="id">شناسه والد</param>
        /// <returns>آیا موفق بود؟</returns>
        bool DeleteParent(long id);
    }
}
