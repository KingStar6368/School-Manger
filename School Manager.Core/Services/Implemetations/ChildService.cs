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
    public class ChildService : IChildService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChildService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public ChildInfo GetChild(long id)
        {
            var result = new ChildInfo();
            
            var ds = _unitOfWork.GetRepository<Child>().Query(
                predicate: p => p.Id == id,
                orderBy: null,
                includes: new List<System.Linq.Expressions.Expression<Func<Child, object>>>
                {
                    c=>c.LocationPairs
                }
                ).FirstOrDefault();
            if (ds != null) 
            {
                result = _mapper.Map<ChildInfo>(ds);
            }

            return result;
        }
        public ChildInfo GetChildByNationCode(string nationCode)
        {
            var result = new ChildInfo();
            var ds = _unitOfWork.GetRepository<Child>().Query()
                    .Include(x=>x.LocationPairs)
                    .FirstOrDefault(p=>p.NationalCode == nationCode.Trim());
            //var ds = _unitOfWork.GetRepository<Child>().Query(
            //    predicate: p => p.NationalCode == nationCode.Trim(),
            //    orderBy: null,
            //    includes: new List<System.Linq.Expressions.Expression<Func<Child, object>>>
            //    {
            //        c=>c.LocationPairs
            //    }
            //    ).FirstOrDefault();
            if (ds != null)
            {
                result = _mapper.Map<ChildInfo>(ds);
            }

            return result;
        }

        public DriverDto GetChildDriver(long ChildId)
        {
            var ds = _unitOfWork.GetRepository<Child>().Query()
                    .Include(c => c.DriverChilds)
                        .ThenInclude(d => d.DriverNavigation)
                            .ThenInclude(d => d.Passanger)
                                .ThenInclude(p => p.ChildNavigation)
                    .Include(c => c.DriverChilds)
                        .ThenInclude(d => d.DriverNavigation)
                            .ThenInclude(d => d.Cars)
                    .FirstOrDefault(c => c.Id == ChildId);
            var driverResult = (ds?.DriverChilds?.FirstOrDefault(x => x.IsEnabled)?.DriverNavigation)?? new Driver();
            return _mapper.Map<DriverDto>(driverResult);
        }

        public async Task<List<ChildInfo>> GetChildren()
        {
            var ds = await _unitOfWork.GetRepository<Child>().Query().Include(x=>x.LocationPairs).ToListAsync();
            return _mapper.Map<List<ChildInfo>>(ds);
        }

        public SchoolDto GetChildSchool(long ChildId)
        {
            var ds = _unitOfWork.GetRepository<Child>()
                .Query()
                .Include(x=>x.SchoolNavigation)
                .FirstOrDefault(x=>x.Id == ChildId);
            return _mapper.Map<SchoolDto>(ds.SchoolNavigation);
        }
    }
}
