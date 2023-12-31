using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.AcademicLevelDTOs
{
    public class AcademicLevelFilterDTO : CorePaginationFilterDTO
    {
        public string Name { get; set; }
        public int LevelHeadId { get; set; }
    }
}
