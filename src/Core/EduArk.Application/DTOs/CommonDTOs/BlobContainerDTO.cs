using Microsoft.AspNetCore.Http;

namespace EduArk.Application.DTOs.CommonDTOs
{
    public class BlobContainerDTO
    {
        public BlobContainerDTO()
        {
            Files = new List<IFormFile>();
        }
        public List<IFormFile> Files { get; set; }

        public int Id { get; set; }
    }
}
