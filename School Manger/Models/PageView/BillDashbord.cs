using School_Manager.Core.ViewModels.FModels;

namespace School_Manger.Models.PageView
{
    public class BillDashbord
    {
        public ParentDto parent {get;set;}
        public ChildInfo child { get; set; }
        public List<BillDto> bills { get; set; }
        public int PDFInfoIndex { get; set; }
    }
}
