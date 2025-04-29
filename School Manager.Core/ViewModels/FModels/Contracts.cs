namespace School_Manger.Models
{
    public class DriverContract
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long CheckPrice { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class ParentContract
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public long TotalPrice { get; set; }
        public long MonthPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class ServiesContract
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class Check
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public long Price { get; set; }
        public string BankName { get; set; }
        public string CheckOwner { get; set; }
        public DateTime CheckTime { get; set; }
    }
}
