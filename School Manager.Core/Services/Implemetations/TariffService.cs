using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Classes;
using School_Manager.Core.Events;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class TariffService : ITariffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICachService _cachService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public TariffService(IUnitOfWork unitOfWork, IMapper mapper, ICachService cachService, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _cachService = cachService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public int CreateTariff(TariffDto dto)
        {
            var tariff = _mapper.Map<Tariff>(dto);
            _unitOfWork.GetRepository<Tariff>().Add(tariff);
            if (_unitOfWork.SaveChanges() > 0)
            {
                _mediator.Publish(new TariffChangeEvent());
                return tariff.Id;
            }
            return 0;
        }

        public async Task<List<TariffDto>> GetActiveTariff()
        {
            var ds = await _cachService.GetOrSetAsync
                (
                 StaticString.TariffList,
                async () => await _unitOfWork.GetRepository<Tariff>().Query(x => x.FromDate < DateTime.Now.AddDays(7) && x.ToDate > DateTime.Now).ToListAsync(),
                absoluteExpireTime: TimeSpan.FromMinutes(30),
                slidingExpireTime: TimeSpan.FromMinutes(10)
                );
            //var ds = await _unitOfWork.GetRepository<Tariff>().Query(x => x.FromDate < DateTime.Now.AddDays(7) && x.ToDate > DateTime.Now).ToListAsync();
            return _mapper.Map<List<TariffDto>>(ds);
        }

        public bool UpdateTariff(TariffDto dto)
        {
            var mainTariff = _unitOfWork.GetRepository<Tariff>().GetById(dto.Id);
            _mapper.Map(dto,mainTariff);
            _unitOfWork.GetRepository<Tariff>().Update(mainTariff);
            if (_unitOfWork.SaveChanges() > 0)
            {
                _mediator.Publish(new TariffChangeEvent());
                return true;
            }
            return false;
        }
    }
}
