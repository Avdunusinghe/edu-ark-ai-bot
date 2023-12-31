using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.ClassDTOs
{
    public class ClassFilterDTO : CorePaginationFilterDTO
    {
        public string? Name { get; set; }
        public int AcademiclLevelId { get; set; }
        public int AcademicYearId { get; set; }
    }
}
