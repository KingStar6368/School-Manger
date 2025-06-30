using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Classes;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.Services.Validations;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class ChildService : IChildService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ChildCreateDto> _createValidator;
        private readonly IValidator<ChildUpdateDto> _UpdateValidator;

        public ChildService(IUnitOfWork unitOfWork,IMapper mapper,IValidator<ChildCreateDto> createValidator, IValidator<ChildUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _UpdateValidator = updateValidator;
        }

        public ChildInfo GetChild(long id)
        {
            var result = new ChildInfo();
            
            var ds = _unitOfWork.GetRepository<Child>()
                                .Query(p => p.Id == id)
                                .Include(x=>x.LocationPairs)
                                .Include(x=>x.ServiceContracts).ThenInclude(x=>x.Bills).ThenInclude(x=>x.PayBills).ThenInclude(x=> x.PayNavigation)
                                .FirstOrDefault();
            if (ds != null) 
            {
                result = _mapper.Map<ChildInfo>(ds);
            }

            return result;
        }
        public ChildInfo GetChildByNationCode(string nationCode)
        {
            var result = new ChildInfo();
            var ds = _unitOfWork.GetRepository<Child>().Query(p=>p.NationalCode == nationCode.Trim())
                    .Include(x=>x.LocationPairs)
                    .Include(x => x.ServiceContracts).ThenInclude(x => x.Bills).ThenInclude(x => x.PayBills).ThenInclude(x => x.PayNavigation)
                    .FirstOrDefault();
            if (ds != null)
            {
                result = _mapper.Map<ChildInfo>(ds);
            }
            else if(ds == null)
            {
                return null;
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

        public async Task<List<ChildInfo>> GetChildWithoutDriver(long DriverId = 0,long SchoolId = 0, double radiusInMeters = 500)
        {
            IQueryable<Child> query = _unitOfWork.GetRepository<Child>().Query(x =>
                !x.DriverChilds.Any(y => y.IsEnabled && y.EndDate > DateTime.Now))
                .Include(x => x.LocationPairs).ThenInclude(x => x.Locations);

            if (SchoolId != 0)
            {
                query = query.Where(x => x.SchoolRef == SchoolId);
            }

            if (DriverId != 0)
            {
                var driver = await _unitOfWork.GetRepository<Driver>().Query(x => x.Id == DriverId).FirstOrDefaultAsync();
                if (driver != null && driver.Latitude != null && driver.Longitude != null)
                {
                    var driverLat = driver.Latitude ?? 34.094092;
                    var driverLng = driver.Longitude ?? 49.697936;

                    query = query.Where(child =>
                        child.LocationPairs.Any(pair =>
                            pair.Locations.Any(loc =>
                                loc.LocationType == Domain.Entities.Catalog.Enums.LocationType.Start &&
                                GeoUtils.Haversine(driverLat, driverLng, loc.Latitude, loc.Longitude) <= radiusInMeters)));
                }
            }

            var ds = await query.ToListAsync();
            return _mapper.Map<List<ChildInfo>>(ds);
        }
        public async Task<List<DriverDto>> GetDriverFree()
        {
            IQueryable<Driver> query = _unitOfWork.GetRepository<Driver>().Query(x =>
                x.AvailableSeats > 0);

            var ds = await query.ToListAsync();
            return _mapper.Map<List<DriverDto>>(ds);
        }
        public long CreateChild(ChildCreateDto child)
        {
            long result = 0;
            var validationResult = _createValidator.Validate(child);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mChild = _mapper.Map<Child>(child);
            _unitOfWork.GetRepository<Child>().Add(mChild);
            if (_unitOfWork.SaveChanges() > 0)
                result = mChild.Id;
            return result;
        }

        public bool DeleteChild(long ChildId)
        {
            var child = _unitOfWork.GetRepository<Child>()
                        .Query(x => x.Id == ChildId)
                        .Include(x => x.ServiceContracts)
                        .FirstOrDefault();

            if (child == null) return false;

            // بررسی وجود اطلاعات وابسته
            if ((child.ServiceContracts?.Any() ?? false))
            {
                throw new InvalidOperationException("این قبض دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }
            _unitOfWork.GetRepository<Child>().Remove(child);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateChild(ChildUpdateDto child)
        {
            var mainchild = _unitOfWork.GetRepository<Child>().GetById(child.Id);
            var validationResult = _UpdateValidator.Validate(child);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            _mapper.Map(child, mainchild);
            _unitOfWork.GetRepository<Child>().Update(mainchild);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool SetDriver(long ChildId, long DriverId)
        {
            var last = _unitOfWork.GetRepository<DriverChild>().Query(x=>x.ChildRef == ChildId && x.IsEnabled).FirstOrDefault();
            if (last != null)
            {
                last.IsEnabled = false;
                last.EndDate = DateTime.Now;
                _unitOfWork.GetRepository<DriverChild>().Update(last);
            }
            var _new = new DriverChild
            {
                DriverRef = DriverId,
                EndDate = DateTime.Now.AddYears(1),
                IsDeleted = false,
                ChildRef = ChildId,
                IsEnabled = true,
                Year = new PersianCalendar().GetYear(DateTime.Now)
            };
            _unitOfWork.GetRepository<DriverChild>().Add(_new);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool SetSchool(long ChildId, long SchoolId)
        {
            var child = _unitOfWork.GetRepository<Child>().Query(x=>x.Id == ChildId).FirstOrDefault();
            if (child == null) return false;
            child.SchoolRef = SchoolId;
            _unitOfWork.GetRepository<Child>().Update(child);
            return _unitOfWork.SaveChanges() > 0;
        }

        public List<ChildInfo> GetChildrenParent(long ParentId)
        {
            var result = new List<ChildInfo>();

            var ds = _unitOfWork.GetRepository<Child>()
                                .Query(p => p.ParentRef == ParentId)
                                .Include(x => x.LocationPairs)
                                .Include(x => x.ServiceContracts).ThenInclude(x => x.Bills).ThenInclude(x => x.PayBills).ThenInclude(x => x.PayNavigation)
                                .ToList();
            if (ds != null)
            {
                result = _mapper.Map<List<ChildInfo>>(ds);
            }

            return result;
        }
    }
}
