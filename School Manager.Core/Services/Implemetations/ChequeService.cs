using AutoMapper;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class ChequeService : IChequeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChequeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public long CreateCheque(ChequeCreateDto cheque)
        {
            long result = 0;
            //var validationResult = _createValidator.Validate(cheque);
            //if (!validationResult.IsValid)
            //{
            //    var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            //    throw new ValidationException(errors);
            //}
            var mcheque = _mapper.Map<Cheque>(cheque);
            _unitOfWork.GetRepository<Cheque>().Add(mcheque);
            if (_unitOfWork.SaveChanges() > 0)
                result = mcheque.Id;
            return result;
        }

        public bool DeleteCheque(long id)
        {
            var cheque = _unitOfWork.GetRepository<Cheque>()
                        .Query(x => x.Id == id)
                        .FirstOrDefault();

            if (cheque == null) return false;

            _unitOfWork.GetRepository<Cheque>().Remove(cheque);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateCheque(ChequeUpdateDto cheque)
        {
            var maincheque = _unitOfWork.GetRepository<Cheque>().GetById(cheque.Id);
            //var validationResult = _UpdateValidator.Validate(cheque);
            //if (!validationResult.IsValid)
            //{
            //    var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            //    throw new ValidationException(errors);
            //}
            _mapper.Map(cheque, maincheque);
            _unitOfWork.GetRepository<Cheque>().Update(maincheque);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
