using School_Manager.Core.ViewModels.FModels;

namespace School_Manger.Models.PageView
{
    public class UserEditDriver
    {
        public DriverUpdateDto DriverUpdateDto { get; set; }
        public UserUpdateDTO UserUpdateDto { get; set; }
    }
    public class UserEditParent
    {
        public ParentUpdateDto DriverUpdateDto { get; set; }
        public UserUpdateDTO UserUpdateDto { get; set; }
    }
}
