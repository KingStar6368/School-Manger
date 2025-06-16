namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس موقعیت
    /// </summary>
    public class LocationDataDto
    {
        /// <summary>
        /// نام آدرس 
        /// </summary>
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
    }
    /// <summary>
    /// کلاس مسیر مسافر از خانه تا مدرسه
    /// </summary>
    public class LocationPairModel
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// کد مسافر
        /// </summary>
        public long ChildId { get; set; }
        /// <summary>
        /// زمان رفت
        /// </summary>
        public DateTime PickTime1 { get; set; }
        /// <summary>
        /// زمان برگشت
        /// </summary>
        public DateTime PickTime2 { get; set; }
        /// <summary>
        /// موقعیت خانه
        /// </summary>
        public LocationDataDto Location1 { get; set; }
        /// <summary>
        /// موقغیت مدرسه
        /// </summary>
        public LocationDataDto Location2 { get; set; }
    }
}
