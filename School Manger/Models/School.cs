namespace School_Manger.Models
{
    public class School
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public int Rate { get; set; } // 0-5
        public LocationData Address { get; set; } = new LocationData();
        public List<long> Drivers { get; set; }
        public List<long> Students { get; set; }
    }
}
