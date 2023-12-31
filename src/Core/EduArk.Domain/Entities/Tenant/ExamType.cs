namespace EduArk.Domain.Entities.Tenant
{
    public class ExamType : BaseEntity
    {
        public ExamType()
        {
            Exams = new HashSet<Exam>();
        }
        public string Name { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
