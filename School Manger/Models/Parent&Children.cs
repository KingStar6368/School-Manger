namespace School_Manger.Models
{
    //Done
    public class Parent
    {
        public long Id { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentNationalCode { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public List<ChildInfo> Children { get; set; }
    }

    //Done
    public class ChildInfo
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string Class { get; set; }
        public long DriverId { get; set; }
        public long SchoolId { get; set; }
        public bool HasPaid
        {
            get => Bills != null && Bills.All(x => x.HasPaId);
        }
        public LocationPairModel Path { get; set; }
        public List<Bill> Bills { get; set; }
    }
}
