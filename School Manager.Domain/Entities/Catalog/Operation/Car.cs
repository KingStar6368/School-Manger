using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Car : AuditableEntity<long>
    {
        /// <summary>
        /// شناسه راننده
        /// </summary>
        public long DriverRef { get; set; }
        /// <summary>
        /// نام ماشین
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// دو رقم اول
        /// </summary>
        public int FirstIntPlateNumber { get; set; }
        /// <summary>
        /// حرف وسط
        /// </summary>
        public string ChrPlateNumber { get; set; }
        /// <summary>
        /// سه رقم بعد از حرف
        /// </summary>
        public int SecondIntPlateNumber { get; set; }
        /// <summary>
        /// کد شهر
        /// </summary>
        public int ThirdIntPlateNumber { get; set; }
        /// <summary>
        /// رنگ
        /// </summary>
        public int? ColorCode { get; set; }
        /// <summary>
        /// فعال
        /// </summary>
        public bool IsActive{ get; set; }
        /// <summary>
        /// تعداد کل صندلی
        /// </summary>
        public int SeatNumber { get; set; }
        /// <summary>
        /// نوع ماشین
        /// </summary>
        public CarType carType { get; set; }
        /// <summary>
        /// راننده
        /// </summary>
        public Driver DriverNavigation { get; set; }

    }
}
