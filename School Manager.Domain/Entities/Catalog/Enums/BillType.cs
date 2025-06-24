using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Enums
{
    public enum BillType
    {
        [Display(Name = "نا مشخص")]
        None = 0,
        [Display(Name = "پیش پرداخت")]
        Pre,
        [Display(Name = "عادی")]
        Normal
    }
}
