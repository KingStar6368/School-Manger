using AutoMapper;
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
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CarService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public long CreateCar(CarCreateDto car)
        {
            long result = 0;
            //var validationResult = _createValidator.Validate(car);
            //if (!validationResult.IsValid)
            //{
            //    var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            //    throw new ValidationException(errors);
            //}
            var mcar = _mapper.Map<Car>(car);
            _unitOfWork.GetRepository<Car>().Add(mcar);
            if (_unitOfWork.SaveChanges() > 0)
                result = mcar.Id;
            return result;
        }

        public bool DeleteCar(long carId)
        {
            var car = _unitOfWork.GetRepository<Car>()
                        .Query(x => x.Id == carId)
                        .FirstOrDefault();

            if (car == null) return false;

            _unitOfWork.GetRepository<Car>().Remove(car);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateCar(CarUpdateDto car)
        {
            var maincar = _unitOfWork.GetRepository<Car>().GetById(car.Id);
            //var validationResult = _UpdateValidator.Validate(car);
            //if (!validationResult.IsValid)
            //{
            //    var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            //    throw new ValidationException(errors);
            //}
            _mapper.Map(car, maincar);
            _unitOfWork.GetRepository<Car>().Update(maincar);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
