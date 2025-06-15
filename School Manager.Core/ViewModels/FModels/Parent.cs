namespace School_Manager.Core.ViewModels.FModels
{
    //Done
    public class ParentDto
    {
        public long Id { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentNationalCode { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public List<ChildInfo> Children { get; set; }
    }
}
