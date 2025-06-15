using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Classes;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Core.Services.Implemetations
{
    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICachService _cachService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public LookupService(IUnitOfWork unitOfWork, IMapper mapper, ICachService cachService, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cachService = cachService;
            _mediator = mediator;
        }

        public LookupComboViewModel? GetLookUp(string Type, int Id)
        {
            var result = new LookupComboViewModel();

            var ds = GetLookupTypesAsync(Type).GetAwaiter().GetResult();

            return ds.FirstOrDefault(x => x.Code == Id);
        }

        public async Task<List<LookupComboViewModel>> GetLookupTypesAsync(string Type)
        {
            var ds = await _cachService.GetOrSetAsync
               (
                   new { CacheKey = StaticString.LookUpBankType },
                   async () => await _unitOfWork.GetRepository<Lookup>().Query
                                        (
                                            predicate: p => p.Type == Type
                                        ).ToListAsync(),
                   absoluteExpireTime: TimeSpan.FromMinutes(55),
                   slidingExpireTime: TimeSpan.FromMinutes(5)
               );
            return _mapper.Map<List<LookupComboViewModel>>(ds);
        }
    }
}
