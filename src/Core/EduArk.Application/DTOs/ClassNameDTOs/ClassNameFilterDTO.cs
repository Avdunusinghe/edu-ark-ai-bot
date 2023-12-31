using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.ClassNameDTOs
{
    public class ClassNameFilterDTO : CorePaginationFilterDTO
    {
        public string? Name { get; set; }
    }
}
