using EduArk.Domain.Entities.Tenant;

namespace EduArk.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
