using AutoMapper;
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
    public class DriverService : IDriverService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public DriverService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public DriverDto GetDriver(long Id)
        {
            throw new NotImplementedException();
        }

        public DriverDto GetDriverNationCode(string NationCode)
        {
            throw new NotImplementedException();
        }

        public List<DriverDto> GetDrivers(int SchoolId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DriverDto>> GetDrivers()
        {
            throw new NotImplementedException();
        }

        public List<ChildInfo> GetPassngers(long id)
        {
            throw new NotImplementedException();
        }
    }
}
