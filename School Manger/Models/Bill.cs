namespace School_Manger.Models
{
    public class Bill
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ContractId { get; set; }
        public long TotalPrice { get; set; }
        public long PaidPrice { get; set; }
        public string Status => (DateTime.Now.Month == BillExpiredTime.Month?"پرداخت نشده":(DateTime.Now < BillExpiredTime?"زمان پرداخت نشده":"تاخیر در پرداخت"));
        public bool HasPaId => (TotalPrice - PaidPrice == 0);
        public DateTime PaidTime { get; set; }
        public DateTime BillExpiredTime { get; set; }
    }
}
