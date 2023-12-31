namespace EduArk.Domain.Entities.Tenant
{
    public class AcademicYear : BaseAuditableEntity
    {
        public AcademicYear()
        {
            Classes = new HashSet<Class>();
            HeadOfDepartments = new HashSet<HeadOfDepartment>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
            SubjectTargetSettings = new HashSet<SubjectTargetSetting>();
            Exams = new HashSet<Exam>();
        }
        public bool IsCurrentYear { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<HeadOfDepartment> HeadOfDepartments { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual ICollection<SubjectTargetSetting> SubjectTargetSettings { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }

    }
}
