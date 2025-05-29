namespace School_Manger.Models.ParentViews
{
    public class BillDashbord
    {
        public Parent parent {get;set;}
        public ChildInfo child { get; set; }
        public List<Bill> bills { get; set; }
        public int PDFInfoIndex { get; set; }
    }
}
