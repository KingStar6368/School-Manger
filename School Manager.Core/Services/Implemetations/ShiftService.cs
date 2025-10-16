using AutoMapper;
using MediatR;
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
    }
}
