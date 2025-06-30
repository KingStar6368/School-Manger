using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manger.Models.PageView
{
    /// <summary>
    /// پنل سمت رانندگان ادمین
    /// </summary>
    public class AdminDriver
    {
        public DriverDto Driver { get; set; }
        public List<ChildInfo> Passanger { get; set; }
    }
    /// <summary>
    /// پنل تصخیص دادن دانش اموز به رانندگان
    /// </summary>
    public class AdminNONChildDriver 
    {
        public List<ChildInfo> NonDivers { get; set; }
        public List<DriverDto> AvailableDrivers { get; set; }
    }
    /// <summary>
    /// پنل سمت خانوادگان
    /// </summary>
    public class AdminParent
    {
        public ParentDto Parent { get; set; }
        public List<DriverDto> Drivers { get; set; }
        public List<SchoolDto> Schools { get; set; }
    }
    /// <summary>
    /// پنل سمت مدارس
    /// </summary>
    public class AdminSchool
    {
        public SchoolDto School { get; set; }
        public List<DriverDto> Drivers { get; set; }
        public List<ChildInfo> Students { get; set; }
    }
    /// <summary>
    /// پنل یوز
    /// </summary>
    public class AdminUser
    {
        public LoginView User { get; set; }
        public DriverDto Driver { get; set; }
        public ParentDto Parent { get; set; }
        public UserType Type { get; set; }
    }
}
