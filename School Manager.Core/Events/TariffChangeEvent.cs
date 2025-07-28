using MediatR;
using School_Manager.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Events
{
    public class TariffChangeEvent : INotification
    {
    }
    public class TariffChangeEventHandler : INotificationHandler<TariffChangeEvent>
    {
        private readonly ICachService _cacheService;
        public TariffChangeEventHandler(ICachService cachService)
        {
            _cacheService = cachService;
        }
        public async Task Handle(TariffChangeEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync(new { CacheKey = "TariffList" });
        }
    }
}
