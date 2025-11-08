using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<SchoolCreateDto> _createValidator;
        private readonly IValidator<SchoolUpdateDto> _UpdateValidator;
        public SchoolService(IUnitOfWork unitOfWork,
                             ICachService cachService,
                             IMapper mapper,
                             IMediator mediator,
                             IValidator<SchoolCreateDto> createValidator,
                             IValidator<SchoolUpdateDto> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _cachService = cachService;
            _mapper = mapper;
            _mediator = mediator;
            _createValidator = createValidator;
            _UpdateValidator = updateValidator;
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
            var ds = await _unitOfWork.GetRepository<Driver>().Query(x => x.DriverShifts.Any(s => s.ShiftNavigation.SchoolRef == id))
                .Include(x=>x.Passanger).ThenInclude(x=>x.ChildNavigation).ThenInclude(x=>x.SchoolNavigation).Include(x=>x.Cars)
                .ToListAsync();
                //.Include(x=>x.Passanger).ThenInclude(x=>x.ChildNavigation).ThenInclude(x=>x.SchoolNavigation)
                //.Where(x=>x.Passanger.Any(y=>y.ChildNavigation.SchoolRef == id)).ToListAsync();
            return _mapper.Map<List<DriverDto>>(ds);
        }

        public SchoolDto GetSchool(long id)
        {
            var ds = _unitOfWork.GetRepository<School>().Query()
                .Include(x => x.Childs).ThenInclude(x => x.DriverChilds).ThenInclude(x => x.DriverShiftNavigation.DriverNavigation)
                .Where(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<SchoolDto>(ds);
        }

        public async Task<List<SchoolDto>> GetSchools()
        {
            var ds = await _unitOfWork.GetRepository<School>().Query()
                .Include(x => x.Childs).ThenInclude(x => x.DriverChilds).ThenInclude(x => x.DriverShiftNavigation.DriverNavigation)
                .ToListAsync();
            return _mapper.Map<List<SchoolDto>>(ds);
        }

        public long CreateSchool(SchoolCreateDto school)
        {
            long result = 0;
            var validationResult = _createValidator.Validate(school);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mSchool = _mapper.Map<School>(school);
            _unitOfWork.GetRepository<School>().Add(mSchool);
            if (_unitOfWork.SaveChanges() > 0)
                result = mSchool.Id;
            return result;
        }
        public bool UpdateSchool(SchoolUpdateDto school)
        {
            var mainSchool = _unitOfWork.GetRepository<School>().GetById(school.Id);
            var validationResult = _UpdateValidator.Validate(school);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            _mapper.Map(school, mainSchool);
            _unitOfWork.GetRepository<School>().Update(mainSchool);
            return _unitOfWork.SaveChanges() > 0;
        }
        public bool DeleteSchool(long id)
        {
            var School = _unitOfWork.GetRepository<School>()
                        .Query(x => x.Id == id)
                        .Include(x => x.Childs)
                        .FirstOrDefault();

            if (School == null) return false;

            // بررسی وجود اطلاعات وابسته
            if (School.Childs?.Any() ?? false)
            {
                throw new InvalidOperationException("این مدرسه دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }
            _unitOfWork.GetRepository<School>().Remove(School);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<List<ShiftDto>> GetSchoolShifts(long id)
        {
            var ds = await _unitOfWork.GetRepository<Shift>().Query(x=>x.SchoolRef == id).ToListAsync();
            return _mapper.Map<List<ShiftDto>>(ds);
        }
    }
}
