namespace EduArk.Domain.Entities.Tenant
{
    public class SubjectTeacher : BaseAuditableEntity
    {
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual User Teacher { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual AcademicLevel AcademicLevel { get; set; }

        public virtual ICollection<HeadOfDepartment> HeadOfDepartments { get; set; }
        public virtual ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; }
    }
}
