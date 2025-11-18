using School_Manager.Core.ViewModels.FModels;

namespace School_Manger.Models.PageView
{
    public class DriverDashbord
    {
        public DriverDto Driver { get; set; }
        public List<ChildInfo> Passanger { get; set; }
        public List<ShiftDto> Shifts { get; set; } // List of available shifts for the driver
    }
}
