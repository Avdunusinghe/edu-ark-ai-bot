namespace EduArk.Domain.Entities.Tenant
{
    public class ExamMark : BaseEntity
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int AcademicLevelId { get; set; }
        public decimal Marks { get; set; }
        public string? Grade { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual AcademicLevel AcademicLevel { get; set; }
     
       


    }
}
