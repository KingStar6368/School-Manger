using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.FModels
{
    public class ChildInfo
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string Class { get; set; }
        public bool HasPaid { get; set; }
        public LocationPairModel Path { get; set; }
    }
}
