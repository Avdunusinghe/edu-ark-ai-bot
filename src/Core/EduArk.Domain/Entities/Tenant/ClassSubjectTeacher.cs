namespace EduArk.Domain.Entities.Tenant
{
    public class ClassSubjectTeacher : BaseAuditableEntity
    {
        public int ClassNameId { get; set; }
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public int SubjectId { get; set; }
        public int SubjectTeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual SubjectTeacher SubjectTeacher { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Class Class { get; set; }
    }
}
