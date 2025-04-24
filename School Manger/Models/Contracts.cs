namespace School_Manger.Models
{
    public class DriverContract
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
    }
    public class ParentContract
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
    }
}
