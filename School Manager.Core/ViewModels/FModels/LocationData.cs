namespace School_Manger.Models
{
    public class LocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }

    public class LocationPairModel
    {
        public long Id { get; set; }
        public long ChildId { get; set; }
        public DateTime PickTime1 { get; set; }
        public DateTime PickTime2 { get; set; }
        public LocationData Location1 { get; set; }
        public LocationData Location2 { get; set; }
    }
}
