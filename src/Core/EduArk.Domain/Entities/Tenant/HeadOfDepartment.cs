namespace EduArk.Domain.Entities.Tenant
{
    public class HeadOfDepartment : BaseAuditableEntity
    {
        public int SubjectId { get; set; }
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public int TeacherId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual SubjectTeacher SubjectTeacher { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual AcademicLevel AcademicLevel { get; set; }

        public virtual ICollection<HeadOfDepartment> HeadOfDepartments { get; set; }

    }
}
