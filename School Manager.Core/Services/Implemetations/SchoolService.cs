using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICachService _cachService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public SchoolService(IUnitOfWork unitOfWork,ICachService cachService,IMapper mapper,IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _cachService = cachService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<ChildInfo>> GetChildren(long id)
        {
            var ds = await _unitOfWork.GetRepository<School>().Query()
                .Include(x=>x.Childs).FirstOrDefaultAsync(x=>x.Id == id);
            var children = ds.Childs.ToList();
            return _mapper.Map<List<ChildInfo>>(children);
        }
        public async Task<List<DriverDto>> GetDrivers(long id)
        {
            var ds = await _unitOfWork.GetRepository<Driver>().Query()
                .Include(x=>x.Passanger).ThenInclude(x=>x.ChildNavigation).ThenInclude(x=>x.SchoolNavigation)
                .Where(x=>x.Passanger.Any(y=>y.ChildNavigation.SchoolRef == id)).ToListAsync();
            return _mapper.Map<List<DriverDto>>(ds);
        }

        public SchoolDto GetSchool(long id)
        {
            var ds = _unitOfWork.GetRepository<School>().Query()
                .Include(x => x.Childs).ThenInclude(x => x.DriverChilds).ThenInclude(x => x.DriverNavigation)
                .Where(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<SchoolDto>(ds);
        }

        public async Task<List<SchoolDto>> GetSchools()
        {
            var ds = await _unitOfWork.GetRepository<School>().Query()
                .Include(x => x.Childs).ThenInclude(x => x.DriverChilds).ThenInclude(x => x.DriverNavigation)
                .ToListAsync();
            return _mapper.Map<List<SchoolDto>>(ds);
        }
    }
}
