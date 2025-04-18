namespace School_Manger.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<ChildInfo> Passanger { get; set; }
        public CarInfo Car { get; set; }
    }
    public class CarInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        public int AvailableSeats { get; set; }
    }
}
