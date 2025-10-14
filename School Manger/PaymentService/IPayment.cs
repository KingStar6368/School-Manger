

namespace School_Manger.PaymentService
{
    public interface IPayment
    {
        public List<PaymentData> GetPayments();
        public void Add(PayData data);
        public PayData Get(string Key);
        public void Clear(string Key);
    }
}
