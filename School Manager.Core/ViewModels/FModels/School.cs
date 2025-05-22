namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس مدرسه
    /// </summary>
    public class School
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام مدسه
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// نام مدیر
        /// </summary>
        public string ManagerName { get; set; } = string.Empty;
        /// <summary>
        /// امتیاز
        /// </summary>
        public int Rate { get; set; } // 0-5
        /// <summary>
        /// آدرس
        /// </summary>
        public LocationDataDto Address { get; set; } = new LocationDataDto();
    }
    /// <summary>
    /// کلاس رانندگان مدرسه
    /// </summary>
    public class SchoolDriverDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام مدرسه
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// نام مدیر
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// آدرس مدرسه
        /// </summary>
        public LocationDataDto Address { get; set; } = new LocationDataDto();
        /// <summary>
        /// لیست از رانندگان این مدرسه
        /// </summary>
        public List<DriverDto> Drivers { get; set; }
    }
    
}
