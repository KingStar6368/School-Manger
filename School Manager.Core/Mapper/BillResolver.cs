using AutoMapper;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Mapper
{
    public class PaidPriceResolver : IValueResolver<Bill, BillDto, long>
    {
        public long Resolve(Bill source, BillDto destination, long destMember, ResolutionContext context)
        {
            return source.PayBills?
                .Where(pb => pb?.PayNavigation != null)
                .Select(pb => pb.PayNavigation.Price)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;
        }
    }
    public class PaidTimeResolver : IValueResolver<Bill, BillDto, DateTime?>
    {
        public DateTime? Resolve(Bill source, BillDto destination, DateTime? destMember, ResolutionContext context)
        {
            return source.PayBills?
                .Where(pb => pb?.PayNavigation?.BecomingTime != null)
                .OrderBy(pb => pb.PayNavigation.BecomingTime)
                .Select(pb => pb.PayNavigation.BecomingTime)
                .LastOrDefault();
        }
    }
    public class HasPaidResolver : IValueResolver<Bill, BillDto, bool>
    {
        public bool Resolve(Bill source, BillDto destination, bool destMember, ResolutionContext context)
        {
            var paid = source.PayBills?
                .Where(pb => pb?.PayNavigation != null)
                .Select(pb => pb.PayNavigation.Price)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

            return paid == source.Price;
        }
    }
    public class StatusResolver : IValueResolver<Bill, BillDto, string>
    {
        public string Resolve(Bill source, BillDto destination, string destMember, ResolutionContext context)
        {
            var paid = source.PayBills?
                .Where(pb => pb?.PayNavigation != null)
                .Select(pb => pb.PayNavigation.Price)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;
            var price = source.Price;
            var estimateDate = source.EstimateTime;
            bool hasPaid = paid == price;
            if (hasPaid) { return "پرداخت شده"; }
            if (estimateDate < DateTime.Now) { return "معوقه"; }
            if (estimateDate > DateTime.Now) { return "سر رسید نشده"; }
            return "نا مشخص";
        }
    }
}
