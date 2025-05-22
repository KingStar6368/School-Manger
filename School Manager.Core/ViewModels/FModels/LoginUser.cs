using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس ورود
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// گذرواژه
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// نوع کاربر
        /// </summary>
        public UserType Type { get; set; }
    }
}
