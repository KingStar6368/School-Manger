using School_Manager.Core.ViewModels.FModels;

namespace School_Manger.Models.PageView
{
    /// <summary>
    /// ViewModel for BillCal page
    /// </summary>
    public class BillCalViewModel
    {
        public BillInstallmentDto Installment { get; set; }
        public List<BillDto> Bills { get; set; }
    }
}
