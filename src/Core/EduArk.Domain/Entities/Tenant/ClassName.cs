namespace EduArk.Domain.Entities.Tenant
{
    public class ClassName : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
