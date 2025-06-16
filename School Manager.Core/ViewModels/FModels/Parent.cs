namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس والدین
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
        /// کد ملی
        /// </summary>
        public string ParentNationalCode { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// فعال است ؟
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// لیست فرزندان
        /// </summary>
        public List<ChildInfo> Children { get; set; }
    }
}
