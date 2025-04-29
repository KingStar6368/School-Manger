namespace School_Manger.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public long TotalPrice { get; set; }
        public long PaidPrice { get; set; }
        public bool HasPaId {  get; set; }
        public DateTime PaidTime { get; set; }
    }
}
