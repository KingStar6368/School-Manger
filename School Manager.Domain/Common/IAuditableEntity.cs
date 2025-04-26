using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Common
{
    public interface IAuditableEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
    public interface IAuditableEntity<TId> where TId : struct
    {
        public TId CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public TId? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}