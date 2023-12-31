namespace EduArk.Domain.Entities.Tenant
{
    public class Exam : BaseAuditableEntity
    {
        public Exam()
        {
            ExamMarks = new HashSet<ExamMark>();
        }

        public string Name { get; set; }
        public int AcademicYearId { get; set; }
        public int ExamTypeId { get; set; }
        public int SemesterId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual ExamType ExamType { get; set; }
        public virtual Semester Semester { get; set; }


        public virtual ICollection<ExamMark> ExamMarks { get; set; }
    }
}
