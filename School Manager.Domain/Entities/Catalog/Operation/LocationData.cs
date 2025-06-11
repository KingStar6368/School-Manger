using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class LocationData : AuditableEntity<long>
    {
        /// <summary>
        /// طول جرافیایی
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
    }
}
