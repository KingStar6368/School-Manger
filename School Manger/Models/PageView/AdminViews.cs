namespace School_Manger.Models.PageView
{
    public class AdminDriver
    {
        public Driver Driver { get; set; }
        public List<ChildInfo> Passanger { get; set; }
    }
    public class AdminParent
    {
        public Parent Parent { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<School> Schools { get; set; }
    }
    public class AdminSchool
    {
        public School School { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<ChildInfo> Students { get; set; }
    }
}
