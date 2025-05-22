using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Core.ViewModels.FModels
{
    /// <summary>
    /// کلاس ماشین
    /// </summary>
    public class CarInfoDto
    {
        /// <summary>
        /// کد
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// نام ماشین
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// پلاک ماشین
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// رنگ
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// صندلی خالی
        /// </summary>
        public int AvailableSeats { get; set; }
        /// <summary>
        /// تعداد کل صندلی
        /// </summary>
        public int SeatNumber { get; set; }
        /// <summary>
        /// نوع ماشین
        /// </summary>
        public CarType carType { get; set; }
    }
}
