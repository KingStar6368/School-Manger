namespace School_Manger.Models
{
    public enum CarType
    {
        Personal,
        Taxi,
        Van,
        Bus
    }
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FutherName { get; set; }
        public DateTime BirthDate {  get; set; }
        public string CertificateId { get; set; }
        public string Education { get; set; }
        public string Descriptions { get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; }
        public string BankNumber { get; set; }
        public string NationCode { get; set; }
        public List<ChildInfo> Passanger { get; set; }
        public CarInfo Car { get; set; }
    }
    public class CarInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get;set; }
        public int AvailableSeats { get; set; }
        public int SeatNumber { get; set; }
    }
}
