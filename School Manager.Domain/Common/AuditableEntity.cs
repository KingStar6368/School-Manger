using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity, ISoftDelete
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

    }

    public abstract class AuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity<TId>, ISoftDelete<TId>
    where TId : struct
    {
        public TId CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public TId? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public TId? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }        
    }

    public abstract class AuditableEntity<UId, TId> : BaseEntity<TId>, IAuditableEntity<UId>, ISoftDelete<UId>
    where UId : struct
    {
        public UId CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public UId? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public UId? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }


}
