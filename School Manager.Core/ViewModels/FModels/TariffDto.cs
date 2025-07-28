using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class TariffDto
    {
        public int Id { get; set; }
        public decimal FromKilometer { get; set; }
        public decimal ToKilometer { get; set; }
        public int Price { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
