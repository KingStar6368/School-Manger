using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IBillService
    {
        /// <summary>
        /// گرفتن لیست قبض ها
        /// </summary>
        /// <returns></returns>
        Task<List<BillDto>> GetBills();
        /// <summary>
        /// گرقتن لیست قبض یک دانش آموز
        /// </summary>
        /// <param name="id">کد دانش آموز</param>
        /// <returns></returns>
        Task<List<BillDto>> GetChildBills(long id);
        /// <summary>
        /// گرفتن یک قبض
        /// </summary>
        /// <param name="id">کد قبض</param>
        /// <returns></returns>
        BillDto GetBill(long id);
        /// <summary>
        /// گرقتن قرار داد قبض
        /// </summary>
        /// <param name="id">کد قبض</param>
        /// <returns></returns>
        ServiceContractDto GetContract(long id);
        /// <summary>
        /// پرداخت شده ؟ 
        /// </summary>
        /// <param name="id">کد قبض</param>
        /// <returns></returns>
        bool IsPiad(long id);
        /// <summary>
        /// ثبت قبض جدید
        /// </summary>
        long Create(BillDto bill);

        /// <summary>
        /// ویرایش قبض
        /// </summary>
        bool Update(BillDto bill);

        /// <summary>
        /// حذف قبض
        /// </summary>
        bool Delete(long billId);

    }
}
