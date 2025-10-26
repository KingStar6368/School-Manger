using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Events;
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
    public class ShiftService : IShiftService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShiftService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public long CreateShift(CreateShiftDto ShiftDto)
        {
            var shift = _mapper.Map<Shift>(ShiftDto);
            _unitOfWork.GetRepository<Shift>().Add(shift);
            if (_unitOfWork.SaveChanges() > 0)
            {
                return shift.Id;
            }
            return 0;
        }

        public bool DeleteShift(int Id)
        {
            var ds = _unitOfWork.GetRepository<Shift>().Query(x => x.Id == Id).FirstOrDefault();
            if (ds != null)
            {
                _unitOfWork.GetRepository<Shift>().Remove(ds);
                int result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool UpdateShift(UpdateShiftDto dto)
        {
            var mainShift = _unitOfWork.GetRepository<Shift>().GetById(dto.Id);
            _mapper.Map(dto, mainShift);
            _unitOfWork.GetRepository<Shift>().Update(mainShift);
            if (_unitOfWork.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public ShiftDto GetShiftById(long id)
        {
            var shift = _unitOfWork.GetRepository<Shift>().GetById(id);
            return _mapper.Map<ShiftDto>(shift);
        }

        public List<ShiftDto> GetAllShifts()
        {
            var shifts = _unitOfWork.GetRepository<Shift>().GetAll();
            return _mapper.Map<List<ShiftDto>>(shifts);
        }

        public List<ShiftDto> GetAllDriverShifts(long DriverId)
        {
            var dshifts = _unitOfWork.GetRepository<DriverShift>().Query(x => x.DriverRef == DriverId).Select(x=>x.ShiftRef);
            var shifts = _unitOfWork.GetRepository<Shift>().Query(x => dshifts.Contains(x.Id)).ToList();
            return _mapper.Map<List<ShiftDto>>(shifts);
        }

        public List<DriverShift> GetDriverShifts(long DriverId)
        {
            var dshifts = _unitOfWork.GetRepository<DriverShift>().Query(x => x.DriverRef == DriverId).ToList();
            return dshifts;
        }

        public List<ShiftDto> GetAllSchoolShifts(long SchoolId)
        {
            var sshifts = _unitOfWork.GetRepository<Shift>().Query(x=>x.SchoolRef == SchoolId).ToList();
            return _mapper.Map<List<ShiftDto>>(sshifts);
        }

        public async Task<List<DriverDto>> GetDriversOfShift(long shiftId)
        {
            var drivers = await _unitOfWork.GetRepository<Driver>()
                .Query(d => d.DriverShifts.Any(ds => ds.ShiftRef == shiftId))
                .Include(d => d.DriverShifts.Where(ds => ds.ShiftRef == shiftId))
                .ThenInclude(d=>d.Passenger)
                .ThenInclude(d=>d.ChildNavigation)
                .ToListAsync();

            return _mapper.Map<List<DriverDto>>(drivers);
        }

        public async Task<List<ChildInfo>> GetChildernOfShift(long ShiftId)
        {
            var Childern = await _unitOfWork.GetRepository<Child>().Query(x => x.ShiftId == ShiftId && !x.DriverChilds.Any(y=>!y.IsEnabled && y.EndDate < DateTime.Now)).ToListAsync();
            return _mapper.Map<List<ChildInfo>>(Childern);
        }
        public async Task<List<ChildInfo>> GetNonDriverChildernOfShift(long ShiftId)
        {
            var Childern = await _unitOfWork.GetRepository<Child>().Query(x => x.ShiftId == ShiftId && !x.DriverChilds.Any(y=>y.IsEnabled && y.EndDate >= DateTime.Now)).ToListAsync();
            return _mapper.Map<List<ChildInfo>>(Childern);
        }

        public DriverShift GetDriverShift(long ShiftId, long DriverId)
        {
            var Shift = _unitOfWork.GetRepository<DriverShift>()
                .Query(x => x.ShiftRef == ShiftId && x.DriverRef == DriverId)
                .AsNoTracking()
                .Include(x => x.Passenger
                    .Where(p => p.IsEnabled && p.EndDate >= DateTime.Now))
                .FirstOrDefault();

            return _mapper.Map<DriverShift>(Shift);
        }

    }
}
