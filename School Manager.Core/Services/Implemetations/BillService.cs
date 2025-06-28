using AutoMapper;
using DNTPersianUtils.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

        public bool Update(BillUpdateDto bill)
        {
            var mainBill = _unitOfWork.GetRepository<Bill>().GetById(bill.Id);
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
                        .FirstOrDefault();

            if (bill == null) return false;

            // بررسی وجود اطلاعات وابسته
            if ((bill.PayBills?.Any() ?? false))
            {
                throw new InvalidOperationException("این قبض دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }
            _unitOfWork.GetRepository<Bill>().Remove(bill);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<SavePreBillResult> CreatePreBill(CreatePreBillDto bill)
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

        //public SavePreBillResult CreatePreBill(CreatePreBillDto bill)
        //{
        //    return CreatePreBillAsync(bill).GetAwaiter().GetResult();
        //}
    }
}
