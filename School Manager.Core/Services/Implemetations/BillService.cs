using AutoMapper;
using DNTPersianUtils.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Classes;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<BillCreateDto> _createValidator;
        private readonly IValidator<BillUpdateDto> _UpdateValidator;
        private readonly IValidator<CreatePreBillDto> _createPreBillValidator;
        public BillService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IValidator<BillCreateDto> createValidator,
                           IValidator<BillUpdateDto> updateValidator,
                           IValidator<CreatePreBillDto> createPreBillValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _UpdateValidator = updateValidator;
            _createPreBillValidator = createPreBillValidator;
        }

        public BillDto GetBill(long id)
        {
            var result = new BillDto();
            var ds = _unitOfWork.GetRepository<Bill>().Query(c=>c.Id == id).Include(c=>c.ServiceContractNavigation)
                .Include(c=>c.PayBills).ThenInclude(c=>c.PayNavigation).FirstOrDefault();
                //predicate: p => p.Id == id,
                //orderBy: null,
                //includes: new List<System.Linq.Expressions.Expression<Func<Bill, object>>>
                //{
                //    c=>c.ServiceContractNavigation,
                //    c=>c.PayBills
                //},
                //new List<Func<IQueryable<Bill>, IQueryable<Bill>>>
                //{
                //    q => q.Include(r=>r.PayBills)
                //        .ThenInclude(d=>d.PayNavigation)
                //}
                //).FirstOrDefault();
            if (ds != null)
            {
               result = _mapper.Map<BillDto>(ds);
            }
            return result;
        }

        public async Task<List<BillDto>> GetBills()
        {
            var result = new List<BillDto>();
            var dss = await _unitOfWork.GetRepository<Bill>().Query().Include(c => c.ServiceContractNavigation)
                .Include(c => c.PayBills).ThenInclude(c => c.PayNavigation).ToListAsync();
        //predicate: p => p.Id > 0,
        //        orderBy: null,
        //        includes: new List<System.Linq.Expressions.Expression<Func<Bill, object>>>
        //        {
        //            c=>c.ServiceContractNavigation,
        //            c=>c.PayBills
        //        },
        //        new List<Func<IQueryable<Bill>, IQueryable<Bill>>>
        //        {
        //            q => q.Include(r=>r.PayBills)
        //                .ThenInclude(d=>d.PayNavigation)
        //        }
        //        ).ToListAsync();
            if (dss != null)
            {
                result = _mapper.Map<List<BillDto>>(dss);
            }
            return result;
        }
        public async Task<List<BillDto>> GetChildBills(long id)
        {
            var result = new List<BillDto>();
            var dss = await _unitOfWork.GetRepository<Bill>().Query(x=>x.ServiceContractNavigation.ChildRef == id).Include(c => c.ServiceContractNavigation)
                .Include(c => c.PayBills).ThenInclude(c => c.PayNavigation).ToListAsync();
            //predicate: p => p.ServiceContractNavigation.ChildRef == id,
            //        orderBy: null,
            //        includes: new List<System.Linq.Expressions.Expression<Func<Bill, object>>>
            //        {
            //            c=>c.ServiceContractNavigation,
            //            c=>c.PayBills
            //        },
            //        new List<Func<IQueryable<Bill>, IQueryable<Bill>>>
            //        {
            //            q => q.Include(r=>r.PayBills)
            //                .ThenInclude(d=>d.PayNavigation)
            //        }
            //        ).ToListAsync();
            if (dss != null)
            {
                result = _mapper.Map<List<BillDto>>(dss);
            }
            return result;
        }

        public ServiceContractDto GetContract(long id)
        {
            var result = new ServiceContractDto();
            var dss =  _unitOfWork.GetRepository<Bill>().Query(c => c.Id == id).Include(c => c.ServiceContractNavigation)
                .Include(c => c.PayBills).ThenInclude(c => c.PayNavigation).FirstOrDefault()?.ServiceContractNavigation;
        //predicate: p => p.Id == id,
        //        orderBy: null,
        //        includes: new List<System.Linq.Expressions.Expression<Func<Bill, object>>>
        //        {
        //            c=>c.ServiceContractNavigation,
        //            c=>c.PayBills
        //        },
        //        new List<Func<IQueryable<Bill>, IQueryable<Bill>>>
        //        {
        //            q => q.Include(r=>r.PayBills)
        //                .ThenInclude(d=>d.PayNavigation)
        //        }
        //        ).FirstOrDefault()?.ServiceContractNavigation;
            if (dss != null)
            {
                result = _mapper.Map<ServiceContractDto>(dss);
            }
            return result;
        }

        public bool IsPiad(long id)
        {
            var mBill = GetBill(id);
            return mBill.HasPaid;
        }

        public long Create(BillCreateDto bill)
        {
            long result = 0;
            var validationResult = _createValidator.Validate(bill);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mBill = _mapper.Map<Bill>(bill);
            _unitOfWork.GetRepository<Bill>().Add(mBill);
            if (_unitOfWork.SaveChanges() > 0)
                result = mBill.Id;
            return result;
        }

        public bool Create(List<BillCreateDto> bills)
        {
            var mBill = _mapper.Map<List<Bill>>(bills);
            _unitOfWork.GetRepository<Bill>().AddRange(mBill);
            return _unitOfWork.SaveChanges() > 0;
        }
        public bool Update(BillUpdateDto bill)
        {
            var mainBill = _unitOfWork.GetRepository<Bill>().Query(x=>x.Id == bill.Id).Include(x=>x.PayBills).FirstOrDefault();
            if(mainBill == null)  return false; 
            if (mainBill.PayBills != null && mainBill.Price != bill.Price)
            {
                throw new InvalidOperationException("این قبض دارای پرداختی است و امکان بروزرسانی مبلغ آن وجود ندارد.");
            }
            var validationResult = _UpdateValidator.Validate(bill);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            _mapper.Map(bill,mainBill);
            _unitOfWork.GetRepository<Bill>().Update(mainBill);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool Delete(long billId)
        {
            var bill = _unitOfWork.GetRepository<Bill>()
                        .Query(x => x.Id == billId)
                        .Include(x => x.PayBills)
                        .Include(x=>x.ServiceContractNavigation)
                        .FirstOrDefault();

            if (bill == null) return false;

            // بررسی وجود اطلاعات وابسته
            if ((bill.PayBills?.Any() ?? false))
            {
                throw new InvalidOperationException("این قبض دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }
            if(bill.Type == BillType.Pre)
            {
                _unitOfWork.GetRepository<ServiceContract>().Remove(bill.ServiceContractNavigation);
            }
            _unitOfWork.GetRepository<Bill>().Remove(bill);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<SavePreBillResult> CreatePreBillAsync(CreatePreBillDto bill)
        {
            SavePreBillResult result = new SavePreBillResult { BillId =0,ServiceContractRef =0};
            var validationResult =await _createPreBillValidator.ValidateAsync(bill);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            
            var saveItem = _mapper.Map<ServiceContract>(bill);
            _unitOfWork.GetRepository<ServiceContract>().Add(saveItem);
            if(_unitOfWork.SaveChanges() > 0)
            {
                result.BillId = saveItem.Bills.FirstOrDefault()?.Id ?? 0;
                result.ServiceContractRef = saveItem.Id;
            }
            return result;
        }

        public SavePreBillResult CreatePreBill(CreatePreBillDto bill)
        {
            return CreatePreBillAsync(bill).GetAwaiter().GetResult();
        }
        public List<BillDto> Create(BillInstallmentDto billInstallmentDto)
        {
            var service = _unitOfWork.GetRepository<ServiceContract>()
                .Query(x => x.Id == billInstallmentDto.ServiceContractRef)
                .Include(x => x.Bills)
                .Include(x => x.ChildNavigation)
                .FirstOrDefault();

            if (service == null) return new List<BillDto>();

            var monthCount = service.ChildNavigation.Class == ClassNumber.Twelfth ? 7 : 8;
            var totalPaid = service.Bills.Select(x => x.Price).DefaultIfEmpty(0).Sum();

            var bills = InstallmentCalculator.CalculateInstallments(
                billInstallmentDto.InstallmentCount,
                billInstallmentDto.Price,
                monthCount,
                totalPaid,
                billInstallmentDto.StartDate,
                billInstallmentDto.EndDate,
                billInstallmentDto.DeadLine,
                billInstallmentDto.ServiceContractRef,
                billInstallmentDto.AddRoundedToFirst,
                billInstallmentDto.RoundePerInstallment
            );

            return _mapper.Map<List<BillDto>>(bills);
        }

        public bool Delete(List<long> billIds)
        {
            var ds = _unitOfWork.GetRepository<Bill>().Query(x=>billIds.Contains(x.Id)).ToList();
            if (ds == null)
            {
                return false;
            }
            if (ds.Any(x => x.PayBills.Any())) return false;
            _unitOfWork.GetRepository<Bill>().RemoveRange(ds);
            return _unitOfWork.SaveChanges() > 0;
        }

        //    public bool Create(BillInstallmentDto billInstallmentDto)
        //    {
        //        var service = _unitOfWork.GetRepository<ServiceContract>()
        //                      .Query(x => x.Id == billInstallmentDto.ServiceContractRef)
        //                      .Include(x=>x.Bills)
        //                      .Include(x=>x.ChildNavigation)
        //                      .FirstOrDefault();
        //        if (service == null) return false;
        //        var MonthCount = 8;
        //        if (service.ChildNavigation.Class == ClassNumber.Twelfth)
        //        {
        //            MonthCount = 7;
        //        }
        //        var totalPrice = (billInstallmentDto.Price * MonthCount) - service.Bills.Select(x=>x.Price).DefaultIfEmpty(0).Sum();
        //        var perInstallment = totalPrice / billInstallmentDto.InstallmentCount;
        //        var remainInstallment = totalPrice % billInstallmentDto.InstallmentCount;
        //        var startYear = billInstallmentDto.StartDate.GetPersianYear();
        //        var startMonth = billInstallmentDto.StartDate.GetPersianMonth();
        //        var endYear = billInstallmentDto.EndDate.GetPersianYear();
        //        var endMonth = billInstallmentDto.EndDate.GetPersianMonth();
        //        var totalMonth = (endMonth - startMonth) + ((endYear - startYear) * 12);
        //        ?
        //        return true;
        //    }
    }
}
