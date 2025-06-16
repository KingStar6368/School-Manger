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
        /// جنسیت
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// فعال است ؟
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// لیست فرزندان
        /// </summary>
        public List<ChildInfo> Children { get; set; }
    }
    public interface IParentDto
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int UserRef { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationalCode { get; set; }
        public bool IsMale {  get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ؟فعال
        /// </summary>
        public bool Active { get; set; }
    }
    public class ParentCreateDto : IParentDto
    {
        public int UserRef {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string NationalCode {get;set;}
        public string Address {get;set;}
        public bool Active {get;set;}
        public bool IsMale { get ; set ; }
    }
    public class ParentUpdateDto : IParentDto
    {
        public long Id { get;set;}
        public int UserRef {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string NationalCode {get;set;}
        public string Address {get;set;}
        public bool Active {get;set;}
        public bool IsMale { get ; set ; }
    }
}
