using School_Manager.Core.ViewModels.FModels;

namespace School_Manger.Models.PageView
{
    public class ParentDashbordView
    {
        public ParentDto Parent { get; set; }
        public ChildInfo SelectedChild { get; set; }
        public List<SchoolDto> Schools { get; set; }
    }
}
