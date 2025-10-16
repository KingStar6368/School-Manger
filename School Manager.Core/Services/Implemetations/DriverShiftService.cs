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
    public class DriverShiftService : IDriverShiftService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DriverShiftService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public long CreateDriverShift(CreateDriverShiftDto ShiftDto)
        {
            var shift = _mapper.Map<DriverShift>(ShiftDto);
            _unitOfWork.GetRepository<DriverShift>().Add(shift);
            if (_unitOfWork.SaveChanges() > 0)
            {
                return shift.Id;
            }
            return 0;
        }

        public bool DeleteDriverShift(int Id)
        {
            var ds = _unitOfWork.GetRepository<DriverShift>().Query(x => x.Id == Id).FirstOrDefault();
            if (ds != null)
            {
                _unitOfWork.GetRepository<DriverShift>().Remove(ds);
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

        public bool UpdateDriverShift(UpdateDriverShiftDto dto)
        {
            var mainShift = _unitOfWork.GetRepository<DriverShift>().GetById(dto.Id);
            _mapper.Map(dto, mainShift);
            _unitOfWork.GetRepository<DriverShift>().Update(mainShift);
            if (_unitOfWork.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
