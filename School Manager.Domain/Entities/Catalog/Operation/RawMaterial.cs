using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Common;
using School_Manager.Domain.Entities.Catalog.Identity;

namespace School_Manager.Domain.Entities.Catalog.Operation
{
    public class RawMaterial : AuditableEntity<int>
    {
        public string MaterialCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public int MaterialNature { get; set; }
        public string TechnicalSpecification { get; set; }
        public int MajorUnitRef { get; set; }
        public int CategoryRef { get; set; }
        public int? SecondaryUnitRef { get; set; }
        public double? UnitConversion { get; set; }
        public int? SupplierRef { get; set; }
    }
}
