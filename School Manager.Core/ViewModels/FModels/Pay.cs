using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public interface IPayDto
    {
        /// <summary>
        /// مبلغ پرداخت
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// تاریخ پرداخت
        /// </summary>
        public DateTime BecomingTime { get; set; }
        /// <summary>
        /// نحوه پرداخت
        /// </summary>
        public PayType PayType { get; set; }
    }
    public class PayCreateDto : IPayDto
    {
        public long Price { get; set; }
        public DateTime BecomingTime { get; set; }
        public PayType PayType { get; set; }
        public List<long> Bills { get; set; }
    }
    public class PayUpdateDto : IPayDto
    {
        public long Price { get; set; }
        public DateTime BecomingTime { get; set; }
        public PayType PayType { get; set; }
    }
    public class PayBillDto
    {
        public long PayRef {  get; set; }
        public long BillRef { get; set; }
    }
}
