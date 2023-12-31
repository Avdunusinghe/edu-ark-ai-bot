namespace EduArk.Domain.Entities.Tenant
{
    public  class EssayQuestion  : BaseAuditableEntity
    {
        
        public int AssessmentId { get; set; }
        public int SequenceNo { get; set; }
        public string Text { get; set; }
        public decimal Marks { get; set; }
       
        
        public virtual Assessment Assessment { get; set; }
    }
}
