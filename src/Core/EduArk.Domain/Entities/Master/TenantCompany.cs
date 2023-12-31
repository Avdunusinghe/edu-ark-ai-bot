namespace EduArk.Domain.Entities.Master
{
    public class TenantCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string? Logo { get; set; }
        public string ConnectionString { get; set; }
        public string? SMTPServer { get; set; }
        public string? SMTPUsername { get; set; }
        public string? SMTPPassword { get; set; }
        public string? SMTPFrom { get; set; }
        public int? SMTPPort { get; set; }
        public bool? IsSMTPUseSSL { get; set; }
        public bool IsGovernmentSchool { get; set; }
        public string? SpecialNotes { get; set; } 
        public Guid TenantId { get; set; }
        public Guid APIKey { get; set; }
        public Guid SecretKey { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
