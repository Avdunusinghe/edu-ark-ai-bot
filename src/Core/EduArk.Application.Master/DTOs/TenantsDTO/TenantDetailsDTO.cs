namespace EduArk.Application.Master.DTOs.TenantsDTO
{
    public class TenantDetailsDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string? DatabaseServer { get; set; }
        public string? Logo { get; set; }
        public string? ServerId { get; set; }
        public string? ConnectionString { get; set; }
        public string? SMTPServer { get; set; }
        public string? SMTPUsername { get; set; }
        public string? SMTPPassword { get; set; }
        public string? SMTPFrom { get; set; }
        public int? SMTPPort { get; set; }
        public bool? IsSMTPUseSSL { get; set; }
        public string? SpecialNotes { get; set; }
        public bool IsGovernmentSchool { get; set; }
    }
}
