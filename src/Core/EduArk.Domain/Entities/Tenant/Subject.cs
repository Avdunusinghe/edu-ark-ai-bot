using EduArk.Domain.Enums;

namespace EduArk.Domain.Entities.Tenant
{
    public class Subject : BaseAuditableEntity
    {
        public Subject()
        {
            ChildBasketSubjects = new HashSet<Subject>();
            HeadOfDepartments = new HashSet<HeadOfDepartment>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
            ClassSubjectTeachers = new HashSet<ClassSubjectTeacher>();
            SubjectAcademicLevels = new HashSet<SubjectAcademicLevel>();
            SubjectTargetSettings = new HashSet<SubjectTargetSetting>();
            ExamMarks = new HashSet<ExamMark>();
        }

        public string Name { get; set; }
        public string SubjectCode { get; set; }
        public SubjectCategory SubjectCategory { get; set; }
        public bool IsParentBasketSubject { get; set; }
        public bool IsBuscketSubject { get; set; }
        public int? ParentBasketSubjectId { get; set; }
        public int SubjectStreamId { get; set; }

        public virtual SubjectStream SubjectStream { get; set; }
        public virtual Subject PerentSubject { get; set; }


        public virtual ICollection<Subject> ChildBasketSubjects { get; set; }
        public virtual ICollection<HeadOfDepartment> HeadOfDepartments { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; }
        public virtual ICollection<SubjectAcademicLevel> SubjectAcademicLevels { get; set; }
        public virtual ICollection<SubjectTargetSetting> SubjectTargetSettings { get; set; }
        public virtual ICollection<ExamMark> ExamMarks { get; set; }
    }
}
