using EduArk.Domain.Enums;

namespace EduArk.Domain.Entities.Tenant
{
    public class Class
    {
        public Class()
        {
            ClassSubjectTeachers = new HashSet<ClassSubjectTeacher>();
            ClassTeachers = new HashSet<ClassTeacher>();
            StudentClasses = new HashSet<StudentClass>();

        }
        public int ClassNameId { get; set; }
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public string Name { get; set; }
        public ClassCategory ClassCategory { get; set; }
        public LanguageStream LanguageStream { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }

        
        public virtual ClassName ClassName { get; set; }
        public virtual AcademicLevel AcademicLevel { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }

        public virtual ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; }
        public virtual ICollection<ClassTeacher> ClassTeachers { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
