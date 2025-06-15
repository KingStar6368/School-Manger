using AutoMapper;
using DNTPersianUtils.Core;
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
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BillService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BillDto GetBill(long id)
        {
            var result = new BillDto();
            var ds = _unitOfWork.GetRepository<Bill>().Query().Include(c=>c.ServiceContractNavigation)
                .Include(c=>c.PayBills).ThenInclude(c=>c.PayNavigation).FirstOrDefault(c=>c.Id == id);
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
            var dss = await _unitOfWork.GetRepository<Bill>().Query().Include(c => c.ServiceContractNavigation)
                .Include(c => c.PayBills).ThenInclude(c => c.PayNavigation).Where(x=>x.ServiceContractNavigation.ChildRef == id).ToListAsync();
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
            var dss =  _unitOfWork.GetRepository<Bill>().Query().Include(c => c.ServiceContractNavigation)
                .Include(c => c.PayBills).ThenInclude(c => c.PayNavigation).FirstOrDefault(c => c.Id == id)?.ServiceContractNavigation;
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

        public long Create(BillDto bill)
        {
            throw new NotImplementedException();
        }

        public bool Update(BillDto bill)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long billId)
        {
            throw new NotImplementedException();
        }
    }
}
