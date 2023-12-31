namespace EduArk.Domain.Entities.Tenant
{
    public class SubjectAcademicLevel
    {
        public int SubjectId { get; set; }
        public int AcademicLevelId { get; set; }

        public virtual AcademicLevel AcademicLevel { get; set; }
        public virtual Subject Subject { get; set; }

        //public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; }
    }
}
