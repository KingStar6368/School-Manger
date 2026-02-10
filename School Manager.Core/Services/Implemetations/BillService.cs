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
            var ds = _unitOfWork.GetRepository<Bill>().Query(c => c.Id == id).Include(c => c.ServiceContractNavigation)
                .Include(c => c.PayBills).ThenInclude(c => c.PayNavigation).FirstOrDefault();

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
            var dss = await _unitOfWork.GetRepository<Bill>().Query(x => x.ServiceContractNavigation.ChildRef == id).Include(c => c.ServiceContractNavigation)
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
            var dss = _unitOfWork.GetRepository<Bill>().Query(c => c.Id == id).Include(c => c.ServiceContractNavigation)
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
            var mainBill = _unitOfWork.GetRepository<Bill>().Query(x => x.Id == bill.Id).Include(x => x.PayBills).FirstOrDefault();
            if (mainBill == null) return false;
            if (mainBill.PayBills != null && mainBill.PayBills.Count > 0 /*mainBill.Price != bill.Price*/)
            {
                throw new InvalidOperationException("این قبض دارای پرداختی است و امکان بروزرسانی مبلغ آن وجود ندارد.");
            }
            var validationResult = _UpdateValidator.Validate(bill);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            _mapper.Map(bill, mainBill);
            _unitOfWork.GetRepository<Bill>().Update(mainBill);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool Delete(long billId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var billRepo = _unitOfWork.GetRepository<Bill>();
                var serviceContractRepo = _unitOfWork.GetRepository<ServiceContract>();

                var bill = billRepo.Query(x => x.Id == billId)
                    .Include(x => x.PayBills)
                    .Include(x => x.ServiceContractNavigation)
                    .FirstOrDefault();
                if (bill.Type == BillType.Pre)
                {
                    var otherbills = billRepo.Query(x => bill.ServiceContractRef == x.ServiceContractRef);
                    if (otherbills.Any(x => x.PayBills.Any() == true))
                        throw new InvalidOperationException("امکان حذف قبض پیش پرداخت وجود ندارد");
                    var serviceContract = bill.ServiceContractNavigation;
                    serviceContractRepo.Remove(serviceContract);
                    _unitOfWork.SaveChanges();
                }
                if (bill == null)
                    throw new InvalidOperationException("قبض یافت نشد.");

                if (bill.PayBills?.Any() ?? false)
                    throw new InvalidOperationException("این قبض دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");

                //var serviceContract = bill.ServiceContractNavigation;

                // جدا کردن navigation قبل از حذف (برای جلوگیری از ارور EF)
                bill.ServiceContractNavigation = null;

                // حذف قبض
                billRepo.Remove(bill);
                _unitOfWork.SaveChanges();

                //// اگر قبض از نوع پیش‌پرداخت بود و قرارداد مرتبط قبض دیگری ندارد، قرارداد را هم حذف کن
                //if (bill.Type == BillType.Pre && serviceContract != null)
                //{
                //    bool hasOtherBills = billRepo.Query(x => x.ServiceContractRef == serviceContract.Id).Any();
                //    if (!hasOtherBills)
                //    {
                //        serviceContractRepo.Remove(serviceContract);
                //        _unitOfWork.SaveChanges();
                //    }
                //}

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new InvalidOperationException($"خطا در اجرای عملیات: {ex.Message}");
            }
        }



        public async Task<SavePreBillResult> CreatePreBillAsync(CreatePreBillDto bill)
        {
            SavePreBillResult result = new SavePreBillResult { BillId = 0, ServiceContractRef = 0 };
            var validationResult = await _createPreBillValidator.ValidateAsync(bill);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            var saveItem = _mapper.Map<ServiceContract>(bill);
            _unitOfWork.GetRepository<ServiceContract>().Add(saveItem);
            if (_unitOfWork.SaveChanges() > 0)
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
            var ds = _unitOfWork.GetRepository<Bill>().Query(x => billIds.Contains(x.Id)).ToList();
            if (ds == null)
            {
                return false;
            }
            if (ds.Any(x => x.PayBills.Any())) return false;
            _unitOfWork.GetRepository<Bill>().RemoveRange(ds);
            return _unitOfWork.SaveChanges() > 0;
        }
        public async Task<List<DebtorDto>> GetNonPaidBill()
        {
            try
            {
                var raw = await _unitOfWork.GetRepository<Parent>()
                    .Query(parent =>
                        parent.Children.Any(children =>
                            children.ServiceContracts.Any(serviceContract =>
                                serviceContract.Bills.Any(bill =>
                                    bill.EstimateTime < DateTime.Now &&
                                    bill.Price > bill.PayBills.Sum(pb => (long?)pb.PayNavigation.Price ?? 0)))))
                    .Select(x => new
                    {
                        Parent = x,
                        ParentId = x.Id,
                        ChildCount = x.Children.Count,
                        ParentFamily = x.LastName,
                        ParentName = x.FirstName,

                        Bills = x.Children
                            .SelectMany(c => c.ServiceContracts)
                            .SelectMany(sc => sc.Bills)
                            .Where(b => b.EstimateTime < DateTime.Now)
                            .Select(b => new
                            {
                                b.Price,
                                Payments = b.PayBills.Sum(pb => (long?)pb.PayNavigation.Price ?? 0)
                            })
                    })
                    .ToListAsync(); // همه چیز تا اینجا توی دیتابیس اجرا میشه

                // اینجا روی حافظه DebtorDto می‌سازیم
                var result = raw.Select(x => new DebtorDto
                {
                    ParentId = x.ParentId,
                    ChildCount = x.ChildCount,
                    ParentFamily = x.ParentFamily,
                    ParentName = x.ParentName,

                    LastSentSMS = x.Parent.UserNavigation?.SMSLogs?
                        .Where(y => y.type == SMSType.Warnning)
                        .OrderByDescending(y => y.SMSTime)
                        .Select(y => y.SMSTime)
                        .FirstOrDefault(),

                    SmsCount = x.Parent.UserNavigation?.SMSLogs?
                        .Count(y => y.type == SMSType.Warnning) ?? 0,

                    Price = x.Bills
                        .Select(b => b.Price - b.Payments)
                        .Where(remain => remain > 0)
                        .Sum()
                })
                .ToList();

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<PayDto>> GetAllPays()
        {
            var ds = await _unitOfWork.GetRepository<Pay>().Query().ToListAsync();
            return _mapper.Map<List<PayDto>>(ds);
        }

        public async Task<List<BillDto>> GetBillsFromPay(long Id)
        {
            var ds = await _unitOfWork.GetRepository<ServiceContract>()
                .Query(s =>
                    s.Bills.Any(b =>
                        b.PayBills.Any(p =>
                            p.Id == Id)))
                .Select(x =>
                    x.Bills.SelectMany(b => b.PayBills)
                ).ToListAsync();
            return _mapper.Map<List<BillDto>>(ds);
        }

        public PayDto GetPay(long id)
        {
            var ds = _unitOfWork.GetRepository<Pay>().Query(x => x.Id == id).ToList();
            return _mapper.Map<PayDto>(ds);
        }

        public async Task<List<BillDto>> SearchBill(SearchDto searchDto)
        {
            var query = _unitOfWork.GetRepository<Bill>().FindAll().Where(x => !x.IsDeleted && x.ServiceContractNavigation != null);
            if (searchDto.StartDate != null)
                query = query.Where(x => x.EstimateTime >= searchDto.StartDate);
            if (searchDto.EndDate != null)
                query = query.Where(x => x.EstimateTime <= searchDto.EndDate);
            if (searchDto.MonthInt != null)
                query = query.Where(x => x.EstimateTime.Month == searchDto.MonthInt);
            if(!string.IsNullOrEmpty(searchDto.BillName))
                query = query.Where(x=>x.Name == searchDto.BillName);
            query = query.Include(x => x.PayBills).ThenInclude(x => x.PayNavigation);

            query = query.Include(x => x.ServiceContractNavigation).ThenInclude(x => x.ChildNavigation);

            query = query
        .OrderByDescending(x => x.CreatedOn)
        .Skip((searchDto.Page - 1) * searchDto.PageSize)
        .Take(searchDto.PageSize);

            var ds = await query.ToListAsync();
            var mapped = _mapper.Map<List<BillDto>>(ds);

            if (searchDto.HasPaid != null)
                mapped = mapped.Where(x => x.HasPaid == searchDto.HasPaid).ToList();

            return mapped;
        }
        public string GetFullNameByBillId(long billid)
        {
            var ds = _unitOfWork.GetRepository<Bill>().Query(x => x.Id == billid)
                .Include(x => x.ServiceContractNavigation)
                .ThenInclude(x => x.ChildNavigation)
                .Select(x => x.ServiceContractNavigation)
                .Select(x => x.ChildNavigation)
                .FirstOrDefault();
            var testds = _unitOfWork.GetRepository<Bill>().Query(x => x.Id == billid).ToList();
            var mapped = _mapper.Map<ChildInfo>(ds);
            return $"{mapped.FirstName} {mapped.LastName}";
        }
    }
}
