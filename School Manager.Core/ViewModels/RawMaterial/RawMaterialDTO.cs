using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.RawMaterial
{
    public class RawMaterialDTO
    {
        public int Id {  get; set; }
        public string MaterialCode { get; set; }
        public string Name { get; set; }
        public string TechnicalSpecification { get; set; }
        public int MajorUnitRef { get; set; }
        public int CategoryRef { get; set; }
        public int MaterialNature { get; set; }
        public int? SecondaryUnitRef { get; set; }
        public double? UnitConversion { get; set; }
    }
}
