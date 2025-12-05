using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Operation;
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
        /// برگرداندن والدین بدهکار
        /// </summary>
        /// <returns></returns>
        Task<List<DebtorDto>> GetNonPaidBill();
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
        long Create(BillCreateDto bill);
        List<BillDto> Create(BillInstallmentDto billDto);
        bool Create(List<BillCreateDto> bills);
        /// <summary>
        /// بروز رسانی قبض
        /// </summary>
        /// <param name="bill">قبض</param>
        /// <returns></returns>
        bool Update(BillUpdateDto bill);
        /// <summary>
        /// حذف قبض
        /// </summary>
        bool Delete(long billId);
        bool Delete(List<long> billIds);
        /// <summary>
        /// ذخیره قبض پیش پرداخت
        /// </summary>
        /// <param name="bill">قبض پیش پرداخت</param>
        /// <returns></returns>
        SavePreBillResult CreatePreBill(CreatePreBillDto bill);
        /// <summary>
        /// دریافت پرداختی ها
        /// </summary>
        /// <returns></returns>
        Task<List<PayDto>> GetAllPays();
        /// <summary>
        /// دریافت قبض ها از طریق پرداختی
        /// </summary>
        /// <param name="Id">کد پرداختی</param>
        /// <returns></returns>
        Task<List<BillDto>> GetBillsFromPay(long Id);
        /// <summary>
        /// دریافت پرداخت با کد
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PayDto GetPay(long id);
        
    }
}
