namespace EduArk.Domain.Entities.Tenant
{
    public class AcademicLevel : BaseAuditableEntity
    {
        public AcademicLevel()
        {
            Classes = new HashSet<Class>();
            HeadOfDepartments = new HashSet<HeadOfDepartment>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
            SubjectAcademicLevels = new HashSet<SubjectAcademicLevel>();
            SubjectTargetSettings = new HashSet<SubjectTargetSetting>();
            ExamMarks = new HashSet<ExamMark>();
        }
        public string Name { get; set; }

        public int LevelHeadId { get; set; }

        public virtual User LevelHead { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<HeadOfDepartment> HeadOfDepartments { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual ICollection<SubjectAcademicLevel> SubjectAcademicLevels { get; set; }
        public virtual ICollection<SubjectTargetSetting> SubjectTargetSettings { get; set; }
        public virtual ICollection<ExamMark> ExamMarks { get; set; }
    }
}
