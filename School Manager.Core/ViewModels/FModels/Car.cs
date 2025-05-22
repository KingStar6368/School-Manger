using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Core.ViewModels.FModels
{

    public class CarInfoDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public int AvailableSeats { get; set; }
        public int SeatNumber { get; set; }
        public CarType carType { get; set; }
    }
}
