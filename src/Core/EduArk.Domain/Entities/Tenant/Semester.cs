namespace EduArk.Domain.Entities.Tenant
{
    public class Semester : BaseEntity
    {
        public Semester()
        {
            SubjectTargetSettings = new HashSet<SubjectTargetSetting>();
            Exams = new HashSet<Exam>();
        }
        public string Name { get; set; }

        public virtual ICollection<SubjectTargetSetting> SubjectTargetSettings { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}

