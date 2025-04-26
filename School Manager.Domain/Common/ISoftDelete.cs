using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Common
{
    public interface ISoftDelete
    {
        DateTime? DeletedOn { get; set; }
        Guid? DeletedBy { get; set; }
        bool IsDeleted { get; set; }
    }

    public interface ISoftDelete<UId> where UId : struct
    {
        DateTime? DeletedOn { get; set; }
        UId? DeletedBy { get; set; }
        bool IsDeleted { get; set; }
    }

}
