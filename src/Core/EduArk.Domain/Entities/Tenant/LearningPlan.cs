namespace EduArk.Domain.Entities.Tenant
{
    public class LearningPlan : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolGrade { get; set; }
        public string StudentMark { get; set; }
        public string AverageMark { get; set; }
        public string LearningPattern { get; set; }

        public virtual ICollection <Lesson>? Lessons { get; set; }
    }
}
