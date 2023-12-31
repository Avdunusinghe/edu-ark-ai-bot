namespace EduArk.Application.DTOs.CommonDTOs
{
    public class BlobResponseDTO
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public BlobContainerDTO Blob { get; set; }
        public string Url { get; set; }
        public BlobResponseDTO()
        {
            Blob = new BlobContainerDTO();
        }
    }
}
