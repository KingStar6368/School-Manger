using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class TarfePriceDto
    {
        /// <summary>
        /// کد تعرفه
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// کیلومتر تعرفه مثال ۵ کیلومتر 
        /// </summary>
        public int Km { get; set; }
        /// <summary>
        /// مبلغ تعرفه
        /// </summary>
        public long Price { get; set; }
    }
}
