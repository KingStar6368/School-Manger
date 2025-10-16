using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Enums
{
    public enum ShiftType
    {
        [Display(Name ="صبح")]
        Morning,
        [Display(Name = "عصر")]
        Evening,
        [Display(Name = "چرخشی")]
        Both
    }
}
