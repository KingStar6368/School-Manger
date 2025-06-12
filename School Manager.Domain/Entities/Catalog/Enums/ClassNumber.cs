using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Entities.Catalog.Enums
{
    public enum ClassNumber
    {
        [Display(Name ="نا مشخص")]
        Unknown = 0,

        [Display(Name = "اول")]
        First = 1,

        [Display(Name = "دوم")]
        Second = 2,

        [Display(Name = "سوم")]
        Third = 3,

        [Display(Name = "چهارم")]
        Fourth = 4,

        [Display(Name = "پنجم")]
        Fifth = 5,

        [Display(Name = "ششم")]
        Sixth = 6,

        [Display(Name = "هفتم")]
        Seventh = 7,

        [Display(Name = "هشتم")]
        Eighth = 8,

        [Display(Name = "نهم")]
        Ninth = 9,

        [Display(Name = "دهم")]
        Tenth = 10,

        [Display(Name = "یازدهم")]
        Eleventh = 11,

        [Display(Name = "دوازدهم")]
        Twelfth = 12
    }
}
