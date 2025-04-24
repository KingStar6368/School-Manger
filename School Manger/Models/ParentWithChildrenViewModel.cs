namespace School_Manger.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentNationalCode { get; set; }
        public bool Active { get; set; }
        public List<ChildInfo> Children { get; set; }
        public long TotalPrice { get; set; }
        //{
        //    get
        //    {
        //        long temp = 0;
        //        foreach(ChildInfo child in Children)
        //        {
        //            if(!child.HasPaid)
        //                temp += child.Price;
        //        }
        //        return temp;
        //    }
        //}
    }

    public class ChildInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string Class { get; set; }
        public bool HasPaid { get; set; }
        public long Price { get; set; }
        public LocationPairModel Path { get; set; }
    }
}
