using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.SubjectDTOs
{
    public class SubjectFilterDTO : CorePaginationFilterDTO
    {
        public string Name { get; set; }
        public int SubjectStreamId { get; set; }
    }
}
