using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class RawMaterialDTO
    {
        public int Id { get; set; }
        public string MaterialCode { get; set; }
        public string Name { get; set; }
        public string TechnicalSpecification { get; set; }
        public int MajorUnitRef { get; set; }
        public int CategoryRef { get; set; }
        public int MaterialNature { get; set; }
        public int? SecondaryUnitRef { get; set; }
        public double? UnitConversion { get; set; }
    }
    public class RawMaterialCombo
    {
        public string DisplayMember { get; set; }
        public int RawMaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string Name { get; set; }
    }

    public class RawMaterialGrid
    {
        public string? RawMaterialName { get; set; }
        public int RawMaterialId { get; set; }
        public string? RawMaterialCode { get; set; }
        public string? MajorUnitName { get; set; }
        public string? SecondaryUnitName { get; set; }
        public double RawMaterialWeight { get; set; }
        public string TechnicalSpecification { get; set; }
    }
    public class RawMaterialDetail
    {
        public RawMaterialDetail()
        {
            //Units = new List<UnitViewModel>();
        }
        public int MajorUnitRef { get; set; }
        public double? UnitConversion { get; set; }
        public double RemainMajor { get; set; }
        public double RemainMinor { get; set; }
        public string TechnicalSpecification { get; set; }
        //public UnitViewModel PrimaryUnit { get; set; }
        //public UnitViewModel SecondaryUnit { get; set; }
        //public List<UnitViewModel> Units { get; set; }
    }
}
