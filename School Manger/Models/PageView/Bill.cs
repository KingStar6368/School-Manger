using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manger.Models.PageView
{
    /// <summary>
    /// ViewModel for BillCal page
    /// </summary>
    public class BillCalViewModel
    {
        public BillInstallmentDto Installment { get; set; }
        public List<BillDto> Bills { get; set; }
        public LocationPairModel Location { get; set; }
    }
}
