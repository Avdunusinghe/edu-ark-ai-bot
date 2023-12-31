namespace EduArk.Domain.Entities.Tenant
{
    public class SubjectStream : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
