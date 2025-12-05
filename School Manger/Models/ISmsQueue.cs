using School_Manager.Core.Services.Interfaces;
using SMS.Base;
using System.Collections.Concurrent;

namespace School_Manger.Models
{

    public interface ISmsQueue
    {
        void Enqueue(string message);
        bool TryDequeue(out string message);
        bool IsBusy { get; }
        void SetBusy(bool busy);
    }

    public class SmsQueue : ISmsQueue
    {
        private readonly ConcurrentQueue<string> _queue = new();
        public bool IsBusy { get; private set; } = false;

        public void Enqueue(string message)
        {
            _queue.Enqueue(message);
        }

        public bool TryDequeue(out string message)
        {
            return _queue.TryDequeue(out message);
        }

        public void SetBusy(bool busy)
        {
            IsBusy = busy;
        }
    }
    public class SmsBackgroundService : BackgroundService
    {
        private readonly ILogger<SmsBackgroundService> _logger;
        private readonly ISmsQueue _smsQueue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ISMSService _smsService;

        public SmsBackgroundService(
            ILogger<SmsBackgroundService> logger,
            ISmsQueue smsQueue,
            IServiceScopeFactory scopeFactory,
            ISMSService smsService)
        {
            _logger = logger;
            _smsQueue = smsQueue;
            _scopeFactory = scopeFactory;
            _smsService = smsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SMS Background Worker Started.");

            using var scope = _scopeFactory.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            string[] mobiles = userService.GetAllParents()
                                          .Select(x => x.Mobile)
                                          .ToArray();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_smsQueue.TryDequeue(out string message))
                {
                    try
                    {
                        _smsQueue.SetBusy(true);

                        await _smsService.Send2All(mobiles, message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending SMS");
                    }
                    finally
                    {
                        _smsQueue.SetBusy(false);
                    }
                }
                else
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }

}
