using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manager.Core.Events;
using MediatR;
using School_Manager.Core.Services.Validations;
using FluentValidation;
using School_Manager.Core.ViewModels.FModels;

namespace School_Manager.Core.Services.Implemetations
{
    public class RawMaterialService : IRawMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICachService _cachService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<RawMaterialDTO> _validator;
        public RawMaterialService(IUnitOfWork unitOfWork,IMapper mapper, ICachService cachService, IMediator mediator, IValidator<RawMaterialDTO> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cachService = cachService;
            _mediator = mediator;
            _validator = validator;
        }

        //public double CheckRemain(int MaterialId,int warehouseId,int TransferId)
        //{
        //    double result = 0;
        //    var ds = _unitOfWork.GetRepository<RawMaterial>().Query(
        //        p => p.Id == MaterialId,
        //        null,
        //        new List<System.Linq.Expressions.Expression<Func<RawMaterial, object>>> 
        //        {
        //            c=>c.IncomingDetails,
        //            c=>c.OutgoingDetails
        //        },
        //        new List<Func<IQueryable<RawMaterial>, IQueryable<RawMaterial>>>
        //        {
        //            q => q.Include(r => r.IncomingDetails)
        //                  .ThenInclude(d => d.IncomingNavigation), // Include Warehouse inside IncomingDetails

        //            q => q.Include(r => r.OutgoingDetails)
        //                  .ThenInclude(d => d.OutgoingNavigation)  // Include Warehouse inside OutgoingDetails
        //        }
        //        ).ToList();
        //    result = ds.Sum(s => s.IncomingDetails.Where(a=>!a.IsDeleted && a.IncomingNavigation.WarehouseRef == warehouseId).Select(s => s.Quantity).DefaultIfEmpty(0).Sum()) -
        //             ds.Sum(s => s.OutgoingDetails.Where(a=>!a.IsDeleted && a.OutgoingNavigation.WarehouseRef == warehouseId && a.Id != TransferId).Select(s => s.Quantity).DefaultIfEmpty(0).Sum());
        //    return result;
        //}

        public double CheckRemain(int materialId, int TransferId)
        {
            throw new NotImplementedException();
        }

        public double CheckRemain(int materialId, int warehouseId, int TransferId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int rawMaterialId)
        {
            var material = _unitOfWork.GetRepository<RawMaterial>().GetById(rawMaterialId);
            _unitOfWork.GetRepository<RawMaterial>().Remove(material);
            return _unitOfWork.SaveChanges() > 0;
        }

        public RawMaterialDetail GetIncomingMaterialDetailAsync(int materialId, int warehouseId, int ReceiptId)
        {
            var single = _unitOfWork.GetRepository<RawMaterial>().Query(
                predicate: p => p.Id == materialId,
                orderBy: null,
                includes: new List<System.Linq.Expressions.Expression<Func<RawMaterial, object>>> {
                    //c=>c.IncomingDetails,
                    //c=>c.OutgoingDetails,
                    //c => c.MajorUnitNavigation,
                    //c=>c.SecondaryUnitNavigation
                },
                new List<Func<IQueryable<RawMaterial>, IQueryable<RawMaterial>>>
                {
                    //q => q.Include(r => r.IncomingDetails)
                    //      .ThenInclude(d => d.IncomingNavigation), // Include Warehouse inside IncomingDetails

                    //q => q.Include(r => r.OutgoingDetails)
                    //      .ThenInclude(d => d.OutgoingNavigation)  // Include Warehouse inside OutgoingDetails
                }
                ).FirstOrDefault();
            double incom = 0, outgo = 0, incomMinor = 0, outgoMinor = 0;
            if (single != null)
            {
                //محاسبه رسیدهای واحد اصلی
                //incom = single.IncomingDetails
                //              .Where(a => a.IncomingNavigation.WarehouseRef == warehouseId && a.Id != ReceiptId && a.UnitRef == single.MajorUnitRef)
                //              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
                ////محاسبه رسیدهای واحد فرعی
                //if (single.SecondaryUnitRef != null)
                //    incomMinor = single.IncomingDetails
                //                  .Where(a => a.IncomingNavigation.WarehouseRef == warehouseId && a.Id != ReceiptId && a.UnitRef == single.SecondaryUnitRef)
                //                  .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
                ////محاسبه حواله های واحد اصلی
                //outgo = single.OutgoingDetails
                //              .Where(a => a.OutgoingNavigation.WarehouseRef == warehouseId &&  a.UnitRef == single.MajorUnitRef)
                //              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
                ////محاسبه حواله های واحد فرعی
                //if (single.SecondaryUnitRef != null)
                //    outgoMinor = single.OutgoingDetails
                //              .Where(a => a.OutgoingNavigation.WarehouseRef == warehouseId && a.UnitRef == single.SecondaryUnitRef)
                //              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
            }
            var inventory = (incom + (incomMinor * single?.UnitConversion ?? 0)) - (outgo + (outgoMinor * single?.UnitConversion ?? 0));
            var result = _mapper.Map<RawMaterialDetail>(single);
            result.RemainMajor = inventory;
            result.RemainMinor = result.UnitConversion == null ? 0 : ((inventory / result.UnitConversion) ?? 0);
            return result;
        }

        public bool GetIncomingMaterialsAsync(int IncomingId)
        {
            throw new NotImplementedException();
        }

        //public bool GetIncomingMaterialsAsync(int IncomingId)
        //{
        //    var incomingList = _unitOfWork.GetRepository<Incoming>().Query(
        //        predicate: p => p.Id == IncomingId,
        //        orderBy: null,
        //        includes: new List<System.Linq.Expressions.Expression<Func<Incoming, object>>> {
        //            c=>c.IncomingDetails
        //        },
        //        new List<Func<IQueryable<Incoming>, IQueryable<Incoming>>>
        //        {
        //            q => q.Include(r => r.IncomingDetails)
        //                  .ThenInclude(d => d.RawMaterialNavigation).ThenInclude(d=>d.OutgoingDetails).ThenInclude(d=>d.OutgoingNavigation),
        //            q=>q.Include(r=>r.IncomingDetails).ThenInclude(d=>d.RawMaterialNavigation).ThenInclude(d=>d.IncomingDetails).ThenInclude(d=>d.IncomingNavigation)

        //        }
        //        ).FirstOrDefault();
        //    if (incomingList == null) return true;
        //    foreach (IncomingDetail item in incomingList.IncomingDetails)
        //    {
        //        double incom = 0, outgo = 0, incomMinor = 0, outgoMinor = 0;
        //        var single = item.RawMaterialNavigation;
        //        if (single != null)
        //        {
        //            //محاسبه رسیدهای واحد اصلی
        //            incom = single.IncomingDetails
        //                   .Where(a => a.IncomingNavigation.WarehouseRef == incomingList.WarehouseRef && a.IncomingRef != IncomingId && a.UnitRef == single.MajorUnitRef)
        //                   .Select(s => s.Quantity)
        //                   .DefaultIfEmpty(0)
        //                   .Sum();
        //            //محاسبه رسیدهای واحد فرعی
        //            if (single.SecondaryUnitRef != null)
        //                incomMinor = single.IncomingDetails
        //                              .Where(a => a.IncomingNavigation.WarehouseRef == incomingList.WarehouseRef && a.IncomingRef != IncomingId && a.UnitRef == single.SecondaryUnitRef)
        //                              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
        //            //محاسبه حواله های واحد اصلی
        //            outgo = single.OutgoingDetails
        //                          .Where(a => a.OutgoingNavigation.WarehouseRef == incomingList.WarehouseRef && a.UnitRef == single.MajorUnitRef)
        //                          .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
        //            //محاسبه حواله های واحد فرعی
        //            if (single.SecondaryUnitRef != null)
        //                outgoMinor = single.OutgoingDetails
        //                          .Where(a => a.OutgoingNavigation.WarehouseRef == incomingList.WarehouseRef && a.UnitRef == single.SecondaryUnitRef)
        //                          .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
        //            var inventory = (incom + (incomMinor * single?.UnitConversion ?? 0)) - (outgo + (outgoMinor * single?.UnitConversion ?? 0));
        //            if (inventory < 0)
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        public RawMaterialDetail GetOutgoingMaterialDetailAsync(int materialId, int warehouseId, int transferId)
        {
            var single =  _unitOfWork.GetRepository<RawMaterial>().Query(
                predicate: p => p.Id == materialId,
                orderBy: null,
                includes: new List<System.Linq.Expressions.Expression<Func<RawMaterial, object>>> {
                    //c=>c.IncomingDetails,
                    //c=>c.OutgoingDetails,
                    //c => c.MajorUnitNavigation,
                    //c=>c.SecondaryUnitNavigation
                },
                new List<Func<IQueryable<RawMaterial>, IQueryable<RawMaterial>>>
                {
                    //q => q.Include(r => r.IncomingDetails)
                    //      .ThenInclude(d => d.IncomingNavigation), // Include Warehouse inside IncomingDetails

                    //q => q.Include(r => r.OutgoingDetails)
                    //      .ThenInclude(d => d.OutgoingNavigation)  // Include Warehouse inside OutgoingDetails
                }
                ).FirstOrDefault();
            double incom = 0, outgo = 0,incomMinor = 0,outgoMinor = 0;
            if (single != null)
            {
                //محاسبه رسیدهای واحد اصلی
                //incom = single.IncomingDetails
                //              .Where(a =>a.IncomingNavigation.WarehouseRef == warehouseId && a.UnitRef == single.MajorUnitRef)
                //              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
                ////محاسبه رسیدهای واحد فرعی
                //if(single.SecondaryUnitRef != null)
                //incomMinor = single.IncomingDetails
                //              .Where(a =>a.IncomingNavigation.WarehouseRef == warehouseId && a.UnitRef == single.SecondaryUnitRef)
                //              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
                ////محاسبه حواله های واحد اصلی
                //outgo = single.OutgoingDetails
                //              .Where(a =>a.OutgoingNavigation.WarehouseRef == warehouseId && a.Id != transferId && a.UnitRef == single.MajorUnitRef)
                //              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
                ////محاسبه حواله های واحد فرعی
                //if (single.SecondaryUnitRef != null)
                //    outgoMinor = single.OutgoingDetails
                //              .Where(a =>a.OutgoingNavigation.WarehouseRef == warehouseId && a.Id != transferId && a.UnitRef == single.SecondaryUnitRef)
                //              .Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
            }
            var inventory = (incom + (incomMinor * single?.UnitConversion??0)) - (outgo + (outgoMinor * single?.UnitConversion??0));
            var result = _mapper.Map<RawMaterialDetail>(single);
            result.RemainMajor = inventory;
            result.RemainMinor = result.UnitConversion == null ? 0 : ((inventory / result.UnitConversion) ?? 0);
            return result;
        }

        public RawMaterialCombo GetRawMaterialComboRow(int Id)
        {
            var ds =  _unitOfWork.GetRepository<RawMaterial>().GetById(Id);
            return _mapper.Map<RawMaterialCombo>(ds);
        }

        public async Task<List<RawMaterialCombo>> GetRawMaterialCombosAsync()
        {
            var ds = await _cachService.GetOrSetAsync
                (
                new { CacheKey = "RawMaterial" },
                async () => await _unitOfWork.GetRepository<RawMaterial>().GetAllAsync(),
                absoluteExpireTime: TimeSpan.FromMinutes(10),
                slidingExpireTime: TimeSpan.FromMinutes(2)
                );
            return _mapper.Map<List<RawMaterialCombo>>(ds);
        }

        public RawMaterialDTO GetRawMaterialDTO(int MaterialId)
        {
            var single = _unitOfWork.GetRepository<RawMaterial>().GetById(MaterialId);
            return _mapper.Map<RawMaterialDTO>(single);
        }

        public async Task<List<RawMaterialGrid>> GetRawMaterialGridAsync()
        {
            var ds = await _unitOfWork.GetRepository<RawMaterial>().Query(
                predicate: p => p.Id > 0,
                orderBy: null,
                includes: new List<System.Linq.Expressions.Expression<Func<RawMaterial, object>>> {
                    //c => c.MajorUnitNavigation,
                    //c=>c.SecondaryUnitNavigation
                }
                ).ToListAsync();
            return _mapper.Map<List<RawMaterialGrid>>(ds);
        }

        public RawMaterial GetSingle(int MaterialId)
        {
            return _unitOfWork.GetRepository<RawMaterial>().GetById(MaterialId);
        }

        public bool HaveData(int Id)
        {
            var ds =  _unitOfWork.GetRepository<RawMaterial>().Query(
                predicate: p => p.Id == Id,
                orderBy: null,
                includes: new List<System.Linq.Expressions.Expression<Func<RawMaterial, object>>> {
                    //c => c.IncomingDetails
                }
                ).FirstOrDefault();
            //return ds.IncomingDetails.Count > 0;
            return true;
        }

        public bool Save(RawMaterialDTO rawMaterialDTO)
        {
            var validationResult = _validator.Validate(rawMaterialDTO);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var material = _mapper.Map<RawMaterial>(rawMaterialDTO);
            _unitOfWork.GetRepository<RawMaterial>().Add(material);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool Update(RawMaterialDTO rawMaterialDTO)
        {
            var material = _unitOfWork.GetRepository<RawMaterial>().GetById(rawMaterialDTO.Id);
            if (material is null)
            {
                return false;
            }
            _mapper.Map(rawMaterialDTO, material);
            _unitOfWork.GetRepository<RawMaterial>().Update(material);
             var res =   _unitOfWork.SaveChanges() > 0;
            _mediator.Publish(new RawMaterialUpdatedEvent { RawMaterialId = rawMaterialDTO.Id });
            return res;
        }
    }
}
