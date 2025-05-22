namespace School_Manger.Models
{
    public class DriverContract
    {
        public long Id { get; set; }
        public Driver DriverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long CheckId { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class ParentContract
    {
        public long Id { get; set; }
        public Parent ParentId { get; set; }
        public ChildInfo ChildId { get; set; }
        public long TotalPrice { get; set; }
        public long MonthPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class ServiesContract
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public long ChildId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class Check
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public long Price { get; set; }
        public string BankName { get; set; }
        public string CheckOwner { get; set; }
        public DateTime CheckTime { get; set; }
    }
}
