namespace School_Manager.Core.ViewModels.FModels
{
    public class DriverContractDto
    {
        public long Id { get; set; }
        public int DriverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long CheckId { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class ParentContractDto
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public long ChildId { get; set; }
        public long TotalPrice { get; set; }
        public long MonthPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class ServiesContractDto
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public long ChildId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] SignatureImage { get; set; }
    }
    public class CheckDto
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public long Price { get; set; }
        public string BankName { get; set; }
        public string CheckOwner { get; set; }
        public DateTime CheckTime { get; set; }
    }
}
