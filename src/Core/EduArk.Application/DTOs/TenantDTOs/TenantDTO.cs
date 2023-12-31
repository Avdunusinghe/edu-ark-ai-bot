namespace EduArk.Application.DTOs.TenantDTOs
{
    public class TenantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Logo { get; set; }
        public string ConnectionString { get; set; }
        public string SMTPServer { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPFrom { get; set; }
        public int SMTPPort { get; set; }
        public bool IsSMTPUseSSL { get; set; }
        public string TenantId { get; set; }
        public string APIKey { get; set; }
        public string SecretKey { get; set; }
        public bool IsActive { get; set; }
    }
}
