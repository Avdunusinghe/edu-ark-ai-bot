using Microsoft.AspNetCore.Http;

namespace EduArk.Application.DTOs.CommonDTOs
{
    public class FileContainerDTO
    {
        public FileContainerDTO()
        {
            Files = new List<IFormFile>();
        }
        public List<IFormFile> Files { get; set; }
        public long Id { get; set; }
        public int Type { get; set; }
    }
}
