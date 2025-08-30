namespace School_Manger.PaymentService
{
    /// <summary>
    /// سرویس دریافت داده پرداختی ها
    /// </summary>
    public class PaymentService : IPayment
    {
        private Dictionary<string,List<long>> PaymentDirectory = new Dictionary<string, List<long>>();
        public PaymentService()
        {

        }

        public void Add(PayData data)
        {
            PaymentDirectory.Add(data.Autratory,data.BillIds);
        }

        public void Clear(string Key)
        {
            PaymentDirectory.Remove(Key);
        }

        public PayData Get(string Key)
        {
            List<long> Ids = new List<long>();
            if (PaymentDirectory.TryGetValue(Key,out Ids))
            {
                return new PayData()
                {
                    Autratory = Key,
                    BillIds = Ids,
                };
            }
            return null;
        }
    }
    /// <summary>
    /// کلاس ذخیره داده خرید درگاه
    /// </summary>
    public class PayData
    {
        /// <summary>
        /// کد دریافتی از درخواست درگاه
        /// </summary>
        public string Autratory { get; set; }
        /// <summary>
        /// لیست قبض های انتخابی
        /// </summary>
        public List<long> BillIds { get; set; }
    }
}
