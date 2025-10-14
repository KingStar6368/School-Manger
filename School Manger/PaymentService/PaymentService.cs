using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using ZarinPal.Class;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace School_Manger.PaymentService
{
    /// <summary>
    /// سرویس دریافت داده پرداختی ها
    /// </summary>
    public class PaymentService : IPayment
    {
        private Dictionary<string, PaymentData> PaymentDirectory = new Dictionary<string, PaymentData>();
        public PaymentService()
        {

        }
        public List<PaymentData> GetPayments()
        {
            return PaymentDirectory.Values.ToList();
        }

        public void Add(PayData data)
        {
            PaymentDirectory.Add(data.Autratory,
                new PaymentData()
                {
                    Authority = data.Autratory,
                    StartedTime = DateTime.Now,
                    BillIds = data.BillIds
                });
        }

        public void Clear(string Key)
        {
            PaymentDirectory.Remove(Key);
        }

        public PayData Get(string Key)
        {
            List<long> Ids = new List<long>();
            if (PaymentDirectory.TryGetValue(Key, out PaymentData data))
            {
                Ids = data.BillIds;
                return new PayData()
                {
                    Autratory = Key,
                    BillIds = Ids,
                };
            }
            return null;
        }
    }
    public class PaymentControl : BackgroundService
    {
        private readonly ILogger<PaymentControl> _logger;
        private readonly IPayment _payment;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(10); // Run every 10 minute

        public PaymentControl(IPayment payment, IServiceProvider serviceProvider, ILogger<PaymentControl> logger)
        {
            _payment = payment;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Payment Control Background Service started. Running every 10 minutes.");

            // Initial delay before first execution (optional)
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var billService = scope.ServiceProvider.GetRequiredService<IBillService>();
                    var zarinPalService = scope.ServiceProvider.GetRequiredService<IZarinPalService>();
                    var billpayment = scope.ServiceProvider.GetRequiredService<IPayBillService>();
                    try
                    {
                        _logger.LogInformation("Payment Control task started at: {time}", DateTime.Now);

                        await ProcessPaymentsAsync(billService, zarinPalService, billpayment, stoppingToken);

                        _logger.LogInformation("Payment Control task completed at: {time}", DateTime.Now);

                        await Task.Delay(_interval, stoppingToken);
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("Payment Control service is stopping gracefully.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred in Payment Control service");
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                    }
                }
            }

            _logger.LogInformation("Payment Control Background Service stopped.");
        }

        private async Task ProcessPaymentsAsync(IBillService billService, IZarinPalService zarinPalService, IPayBillService billpayment, CancellationToken stoppingToken)
        {
            foreach (var payment in _payment.GetPayments())
            {
                if (payment.Worn >= 3)
                {
                    _payment.Clear(payment.Authority);
                    continue;
                }
                if (DateTime.Now - payment.StartedTime < _interval)
                    continue;
                var totalPrice = payment.BillIds
                    .Select(x => billService.GetBill(x).TotalPrice - billService.GetBill(x).PaidPrice)
                    .Sum();
                int StatusCode = await zarinPalService.VerfiyPaymentAsync(int.Parse(totalPrice.ToString()), payment.Authority);
                if (StatusCode == 100 || StatusCode == 101)
                {
                    _payment.Clear(payment.Authority);
                    foreach (var billId in payment.BillIds)
                    {
                        bool alreadyPaid = billpayment
                            .GetAllPays(billId)
                            .Any(x => x.TrackingCode == payment.Authority.Replace("0000", ""));

                        if (!alreadyPaid)
                        {
                            billpayment.CreatePay(new School_Manager.Core.ViewModels.FModels.PayCreateDto()
                            {
                                BecomingTime = DateTime.Now,
                                PayType = School_Manager.Domain.Entities.Catalog.Enums.PayType.Internet,
                                Bills = payment.BillIds,
                                Price = totalPrice,
                                TrackingCode = payment.Authority.Replace("0000", "")
                            });
                        }
                    }
                }

                _logger.LogInformation("Processing payments...");
                await Task.Delay(1000, stoppingToken); // Simulate work
            }
        }
    }
    /// <summary>
    /// کلاس پرداخت های درحال انجام و یا انجام شده و به سایت مراجعه نکرده اند
    /// </summary>
    public class PaymentData
    {
        /// <summary>
        /// شناسه یکتا
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// لیست قبض ها
        /// </summary>
        public List<long> BillIds { get; set; }
        /// <summary>
        /// زمان درخواست
        /// </summary>
        public DateTime StartedTime { get; set; }
        /// <summary>
        /// تعداد برسی توسط سرویس
        /// </summary>
        public int Worn { get; set; } = 0;
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
