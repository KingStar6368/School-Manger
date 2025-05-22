namespace School_Manager.Core.ViewModels.FModels
{
    public class School
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;
        public int Rate { get; set; } // 0-5
        public LocationDataDto Address { get; set; } = new LocationDataDto();
    }
    public class SchoolDriverDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public int Rate { get; set; }
        public LocationDataDto Address { get; set; } = new LocationDataDto();
        public List<DriverDto> Drivers { get; set; }
    }
    
}
