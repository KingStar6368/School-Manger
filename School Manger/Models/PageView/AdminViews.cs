namespace School_Manger.Models.PageView
{
    /// <summary>
    /// پنل سمت رانندگان ادمین
    /// </summary>
    public class AdminDriver
    {
        public Driver Driver { get; set; }
        public List<ChildInfo> Passanger { get; set; }
    }
    /// <summary>
    /// پنل تصخیص دادن دانش اموز به رانندگان
    /// </summary>
    public class AdminNONChildDriver 
    {
        public List<ChildInfo> NonDivers { get; set; }
        public List<Driver> AvailableDrivers { get; set; }
    }
    /// <summary>
    /// پنل سمت خانوادگان
    /// </summary>
    public class AdminParent
    {
        public Parent Parent { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<School> Schools { get; set; }
    }
    /// <summary>
    /// پنل سمت مدارس
    /// </summary>
    public class AdminSchool
    {
        public School School { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<ChildInfo> Students { get; set; }
    }
}
