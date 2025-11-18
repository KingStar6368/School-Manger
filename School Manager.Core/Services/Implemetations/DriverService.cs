using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<DriverCreateDto> _createValidator;
        private readonly IValidator<DriverUpdateDto> _UpdateValidator;

        public DriverService(IUnitOfWork unitOfWork,IMapper mapper, IValidator<DriverCreateDto> createValidator, IValidator<DriverUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _UpdateValidator = updateValidator;
        }

        public DriverDto GetDriver(long Id)
        {
            var ds = _unitOfWork.GetRepository<Driver>()
                .Query()
                .Include(x=>x.DriverShifts).ThenInclude(x=>x.Passenger).ThenInclude(x=>x.ChildNavigation)
                .Include(x => x.Cars)
                .Include(x => x.Passanger).ThenInclude(x => x.ChildNavigation)
                .FirstOrDefault(x=>x.Id == Id /*&& x.Passanger.Any(y=>y.IsEnabled)*/);
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

        public async Task<List<DriverDto>> GetDrivers(long schoolId)
        {
            var school = await _unitOfWork.GetRepository<School>()
                .Query()
                .Include(x => x.Childs)
                    .ThenInclude(x => x.DriverChilds)
                        .ThenInclude(x => x.DriverShiftNavigation)
                            .ThenInclude(x => x.DriverNavigation)
                                .ThenInclude(x => x.Cars)
                .FirstOrDefaultAsync(x => x.Id == schoolId);

            if (school == null)
                return new List<DriverDto>();

            var drivers = school.Childs
                .SelectMany(c => c.DriverChilds)
                .Where(dc => dc.DriverShiftNavigation != null && dc.DriverShiftNavigation.DriverNavigation != null)
                .Select(dc => dc.DriverShiftNavigation.DriverNavigation)
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

        public List<ChildInfo> GetPassngers(long DriverShiftId)
        {
            var child = _unitOfWork.GetRepository<Child>().Query(x => x.DriverChilds.Any(y=>y.DriverShiftRef == DriverShiftId && y.IsEnabled && y.EndDate >= DateTime.Now))
                .Include(x=>x.LocationPairs)
                .ThenInclude(x=>x.Locations).ToList();
            return _mapper.Map<List<ChildInfo>>(child);
        }

        public async Task<long> CreateDriverAsync(DriverCreateDto driver)
        {
            long result = 0;
            var validationResult = await _createValidator.ValidateAsync(driver);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mDriver = _mapper.Map<Driver>(driver);
            _unitOfWork.GetRepository<Driver>().Add(mDriver);
            if (_unitOfWork.SaveChanges() > 0)
                result = mDriver.Id;
            return result;
        }

        public bool DeleteDriver(long id)
        {
            var Driver = _unitOfWork.GetRepository<Driver>()
                        .Query(x => x.Id == id)
                        .Include(x => x.DriverContracts)
                        .Include(x=>x.Passanger)
                        .FirstOrDefault();

            if (Driver == null) return false;

            // بررسی وجود اطلاعات وابسته
            if ((Driver.DriverContracts?.Any() ?? false) && Driver.Passanger.Any(x=>x.IsEnabled && x.EndDate > DateTime.Now))
            {
                throw new InvalidOperationException("این قبض دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }
            _unitOfWork.GetRepository<Driver>().Remove(Driver);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> UpdateDriverAsync(DriverUpdateDto driver)
        {
            var mainDriver = _unitOfWork.GetRepository<Driver>().GetById(driver.Id);
            var validationResult = await _UpdateValidator.ValidateAsync(driver);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            _mapper.Map(driver, mainDriver);
            _unitOfWork.GetRepository<Driver>().Update(mainDriver);
            return _unitOfWork.SaveChanges() > 0;
        }
        public long CreateDriver(DriverCreateDto driver)
        {
            return CreateDriverAsync(driver).Result;
        }

        public bool UpdateDriver(DriverUpdateDto driver)
        {
            return UpdateDriverAsync(driver).Result;
        }

        public bool DeleteDriverByUserId(long userId)
        {
            var Driver = _unitOfWork.GetRepository<Driver>()
                        .Query(x => x.UserRef == userId)
                        .Include(x => x.DriverContracts)
                        .Include(x => x.Passanger)
                        .FirstOrDefault();

            if (Driver == null) return false;

            // بررسی وجود اطلاعات وابسته
            if ((Driver.DriverContracts?.Any() ?? false) && Driver.Passanger.Any(x => x.IsEnabled && x.EndDate > DateTime.Now))
            {
                throw new InvalidOperationException("این قبض دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }
            _unitOfWork.GetRepository<Driver>().Remove(Driver);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<List<DriverDto>> SearchDriver(SearchDto filter)
        {
            IQueryable <Driver> query = _unitOfWork.GetRepository<Driver>()
                            .FindAll()
                            .Include(x => x.Cars);

            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(p => p.Name.Contains(filter.FirstName));

            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(p => p.LastName.Contains(filter.LastName));

            if (!string.IsNullOrEmpty(filter.NationalCode))
                query = query.Where(p => p.NationCode.Contains(filter.NationalCode));

            if (!string.IsNullOrEmpty(filter.Mobile))
                query = query.Where(p => p.UserNavigation.Mobile.Contains(filter.Mobile));

            var result = await query.ToListAsync();

            return _mapper.Map<List<DriverDto>>(result);
        }
    }
}
