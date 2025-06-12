using AutoMapper;
using MediatR;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
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

        public Task<List<ChildInfo>> GetChildren(long id)
        {
            throw new NotImplementedException();
        }

        public List<SchoolDriverDto> GetDrivers()
        {
            throw new NotImplementedException();
        }

        public Task<List<SchoolDriverDto>> GetDrivers(long id)
        {
            throw new NotImplementedException();
        }

        public School GetSchool(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<School>> GetSchools()
        {
            throw new NotImplementedException();
        }
    }
}
