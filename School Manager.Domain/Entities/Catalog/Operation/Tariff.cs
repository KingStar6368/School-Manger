using School_Manager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class Tariff : AuditableEntity<int>
    {
        public decimal FromKilometer { get; set; }
        public decimal ToKilometer { get;set; }
        public int Price { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
