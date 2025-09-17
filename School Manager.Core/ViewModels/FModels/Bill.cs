using DNTPersianUtils.Core;
using School_Manager.Core.Utilities;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس قبض
    /// </summary>
    public class BillDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// عنوان قبض
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// کد قرار داد
        /// </summary>
        public long ContractId { get; set; }
        /// <summary>
        /// مبلغ کل قبض
        /// </summary>
        public long TotalPrice { get; set; }
        /// <summary>
        /// مبلغ پرداخت شده
        /// </summary>
        public long PaidPrice { get; set; }
        /// <summary>
        /// وضعیت پرداخت
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// متن وضعیت
        /// </summary>
        public string TypeOfBill {  get; set; }
        /// <summary>
        /// پرداخت شده؟
        /// </summary>
        public bool HasPaid {  get; set; }
        /// <summary>
        /// زمان پرداخت کامل
        /// </summary>
        public DateTime? PaidTime { get; set; }
        /// <summary>
        /// تاریخ انقضای فرار داد
        /// </summary>
        public DateTime BillExpiredTime { get; set; }
        /// <summary>
        /// تاریخ انقضای فرار داد
        /// </summary>
        public string BillExpiredTimePer { get; set; }
    }
    public class BillInstallmentDto
    {
        /// <summary>
        /// تعداد قسط
        /// </summary>
        public int InstallmentCount { get; set; } = 6;
        /// <summary>
        /// مبلغ ماهیانه
        /// </summary>
        public int Price { get; set; } = 15000000;
        /// <summary>
        /// آی دی قرارداد
        /// </summary>
        public long ServiceContractRef {  get; set; }
        /// <summary>
        /// تاریخ اولین قبض
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now.GetPersianYearStartAndEndDates(true).StartDate.AddMonths(7);
        /// <summary>
        /// تاریخ آخرین قبض اگر تاریخی وارد نشود آخرین روز سال را در نظر می گیرد
        /// </summary>
        public DateTime EndDate { get; set; } = DateTime.Now.GetPersianYearStartAndEndDates(true).EndDate;
        /// <summary>
        /// تعداد روزها برای مهلت پرداخت
        /// </summary>
        public int DeadLine { get; set; } = 5;
    }
    public interface IBillDto
    {
        public string Name { get; set; }
        /// <summary>
        /// کد قرار داد
        /// </summary>
        public long ServiceContractRef { get; set; }
        /// <summary>
        /// مبلغ کل قبض
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// مهلت پرداخت
        /// </summary>
        public DateTime EstimateTime { get; set; }
        public int Type {  get; set; }

    }
    public class BillCreateDto : IBillDto
    {
        public long ServiceContractRef { get; set; }
        public long Price { get; set; }
        public DateTime EstimateTime { get; set; }
        public string Name {get;set;}
        public int Type { get; set; }
    }
    public class BillUpdateDto : IBillDto
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کد قرار داد
        /// </summary>
        public long ServiceContractRef { get; set; }
        /// <summary>
        /// مبلغ کل قبض
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// مهلت پرداخت
        /// </summary>
        public DateTime EstimateTime { get; set; }
        public string Name {get;set;}
        public int Type { get; set; }
    }
    public class CreatePreBillDto
    {
        public long ChildRef {  get; set; }
        public string Name { get; set; } = Constant.PreBillName;
        public DateTime EstimateTime { get; set; } = DateTime.Now;
        public DateTime StartTime {  get; set; }
        public DateTime EndTime { get; set; }
        public long Price { get; set; }
    }
    /// <summary>
    /// نتیجه ذخیره قبض پیش پرداخت
    /// </summary>
    public class SavePreBillResult
    {
        public long BillId { get; set; }
        public long ServiceContractRef { get; set; }
    }
}
