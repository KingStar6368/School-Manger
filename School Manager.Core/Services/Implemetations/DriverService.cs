using AutoMapper;
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
            var ds = _unitOfWork.GetRepository<Driver>()
                .Query()
                .Include(x => x.Cars)
                .Include(x => x.Passanger).ThenInclude(x => x.ChildNavigation)
                .FirstOrDefault(x=>x.Id == Id);
            return _mapper.Map<DriverDto>(ds);
        }

        public DriverDto GetDriverNationCode(string NationCode)
        {
            var ds = _unitOfWork.GetRepository<Driver>()
                .Query()
                .Include(x => x.Cars)
                .Include(x => x.Passanger).ThenInclude(x => x.ChildNavigation)
                .FirstOrDefault(x => x.NationCode == NationCode.Trim());
            return _mapper.Map<DriverDto>(ds);
        }

        public async Task<List<DriverDto>> GetDrivers(int SchoolId)
        {
            var ds = await _unitOfWork.GetRepository<School>()
                .Query()
                .Include(x => x.Childs).ThenInclude(x => x.DriverChilds).ThenInclude(x => x.DriverNavigation).ThenInclude(x=>x.Cars)
                .Where(x => x.Id == SchoolId).FirstOrDefaultAsync();
            var drivers = ds.Childs
                        .SelectMany(c => c.DriverChilds)
                        .Where(dc => dc.DriverNavigation != null)
                        .Select(dc => dc.DriverNavigation)
                        .Distinct()
                        .ToList();
            return _mapper.Map<List<DriverDto>>(drivers);
        }

        public async Task<List<DriverDto>> GetDrivers()
        {
            var ds = await _unitOfWork.GetRepository<Driver>().Query()
                .Include(x => x.Cars)
                .Include(x => x.Passanger).ThenInclude(x => x.ChildNavigation).ToListAsync();
            return _mapper.Map<List<DriverDto>>(ds);
        }

        public List<ChildInfo> GetPassngers(long id)
        {
            var ds =  _unitOfWork.GetRepository<Driver>().Query()
                .Include(x => x.Passanger).ThenInclude(x => x.ChildNavigation).FirstOrDefault(x=>x.Id == id);
            var child = ds.Passanger.Select(x => x.ChildNavigation).ToList();
            return _mapper.Map<List<ChildInfo>>(child);
        }
    }
}
