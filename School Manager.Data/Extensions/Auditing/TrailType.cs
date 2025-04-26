using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Data.Extensions.Auditing
{
    public enum TrailType : byte
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
