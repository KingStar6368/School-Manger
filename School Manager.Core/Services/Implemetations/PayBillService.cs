using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Core.Services.Implemetations
{
    public class PayBillService : IPayBillService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PayCreateDto> _createValidator;
        private readonly IMapper _mapper;
        public PayBillService(IUnitOfWork unitOfWork,IValidator<PayCreateDto> createValidator,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _mapper = mapper;
        }
        public long CreatePay(PayCreateDto pay)
        {
            long result = 0;
            var validationResult = _createValidator.Validate(pay);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var saveItem = _mapper.Map<Pay>(pay);
            _unitOfWork.GetRepository<Pay>().Add(saveItem);
            if(_unitOfWork.SaveChanges() > 0)
            {
                result = saveItem.Id;
            }
            return result;
        }

        public bool DeletePay(long PayId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var Pay = _unitOfWork.GetRepository<Pay>()
                            .Query(x => x.Id == PayId)
                            .FirstOrDefault();

                if (Pay == null) return false;

                var payBills = _unitOfWork.GetRepository<PayBill>().Query(x => x.Id == PayId).ToList();
                _unitOfWork.GetRepository<PayBill>().RemoveRange(payBills);
                _unitOfWork.GetRepository<Pay>().Remove(Pay);
                var saveResult = _unitOfWork.SaveChanges() > 0;
                _unitOfWork.Commit();
                return saveResult;
            }
            catch
            {
                _unitOfWork.Rollback();
                return false;
            }
        }

        public List<PayBillDto> GetAllPays(long BillId)
        {
            var result = _unitOfWork.GetRepository<PayBill>().Query(x=>x.BillRef ==  BillId).Include(x=>x.PayNavigation).ToList();
            return _mapper.Map<List<PayBillDto>>(result);
        }

        public PayBillDto GetPay(long Id)
        {
            var result = _unitOfWork.GetRepository<PayBill>().Query(x => x.Id == Id).Include(x => x.PayNavigation).FirstOrDefault();
            return _mapper.Map<PayBillDto>(result);
        }
    }
}
