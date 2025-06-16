namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس مدرسه
    /// </summary>
    public class SchoolDto
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
        /// لیست از رانندگان این مدرسه NewC
        /// </summary>
        public List<long> Drivers { get; set; }
        /// <summary>
        /// کد دانش آموزان NewV
        /// </summary>
        public List<long> Students { get; set; }
    }

}
