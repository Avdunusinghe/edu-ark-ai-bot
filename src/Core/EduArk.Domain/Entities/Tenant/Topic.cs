namespace EduArk.Domain.Entities.Tenant
{
    public  class Topic : BaseAuditableEntity
    {
       
        public int LessonId { get; set; }
        public string Name { get; set; }
        public int SequenceNo { get; set; }
        public string LearningExperience { get; set; }
        
        public virtual ICollection<Assessment> Assessments { get; set; }
    }
}
