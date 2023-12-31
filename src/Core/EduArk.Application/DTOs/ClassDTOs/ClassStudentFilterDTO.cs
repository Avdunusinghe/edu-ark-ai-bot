using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.ClassDTOs
{
    public class ClassStudentFilterDTO : CorePaginationFilterDTO
    {
        public string? Name { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int ClassNameId { get; set; }
    }
}
