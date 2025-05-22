namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس والد
    /// </summary>
    public class ParentDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string ParentFirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string ParentLastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string ParentNationalCode { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ؟فعال
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// لیست فرزندها
        /// </summary>
        public List<ChildInfo> Children { get; set; }
    }
}
