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

            var ds = _unitOfWork.GetRepository<Child>().Query(
                predicate: p => p.NationalCode == nationCode.Trim(),
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

        public DriverDto GetChildDriver(long ChildId)
        {
            var result = new DriverDto();

            var ds = _unitOfWork.GetRepository<Child>().Query(
                predicate: p => p.Id == ChildId,
                orderBy: null,
                includes: new List<System.Linq.Expressions.Expression<Func<Child, object>>>
                {
                    c=>c.DriverChildNavigation
                },
                thenIncludes:
                new List<Func<IQueryable<Child>, IQueryable<Child>>>
                {
                    q => q.Include(r=>r.DriverChildNavigation)
                        .ThenInclude(d=>d.DriverNavigation).ThenInclude(d=>d.Passanger).ThenInclude(d=>d.ChildNavigation),
                    q=>q.Include(r=>r.DriverChildNavigation)
                    .ThenInclude(r=>r.DriverNavigation).ThenInclude(r=>r.Cars)
                }).FirstOrDefault();
            var driverResult = ds.DriverChildNavigation.FirstOrDefault(x => x.IsEnabled).DriverNavigation;
            return result;
        }

        public Task<List<ChildInfo>> GetChildren()
        {
            throw new NotImplementedException();
        }

        public SchoolDto GetChildSchool(long SchoolId)
        {
            throw new NotImplementedException();
        }

        SchoolDto IChildService.GetChildSchool(long SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
