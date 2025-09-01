using MediatR;
using School_Manager.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Events
{
    public class RawMaterialUpdatedEvent : INotification
    {
        public int RawMaterialId { get; set; }
    }
    public class RawMaterialUpdatedEventHandler : INotificationHandler<RawMaterialUpdatedEvent>
    {
        private readonly ICachService _cacheService;

        public RawMaterialUpdatedEventHandler(ICachService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(RawMaterialUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync("RawMaterial");
        }
    }
}
