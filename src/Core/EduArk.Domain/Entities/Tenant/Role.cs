using EduArk.Domain.Common;

namespace EduArk.Domain.Entities.Tenant
{
    public class Role : BaseAuditableEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
