namespace EduArk.Application.DTOs.CommonDTOs
{
    public class FileDTO
    {
        public MemoryStream FileContent { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
    }
}
