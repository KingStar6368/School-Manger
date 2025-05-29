namespace School_Manger.Models.ParentViews
{
    public class ParentDashbordView
    {
        public Parent Parent { get; set; }
        public ChildInfo SelectedChild { get; set; }
        public List<School> Schools { get; set; }
    }
}
