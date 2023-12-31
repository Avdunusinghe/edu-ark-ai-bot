namespace EduArk.Domain.Entities.Tenant
{
    public class StructuredQuestion : BaseAuditableEntity
    {
        
        public string AssessmentId { get; set; }
        public int SequenceNo { get; set; }
        public string Text { get; set; }
        public decimal Marks { get; set; }

        public virtual Assessment Assessment { get; set; }

    }
}
