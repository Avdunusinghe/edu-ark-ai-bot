using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.SubjectTeachersDTOs
{
    public class SubjectTeacherFilterDTO 
    {
        public string? SubjectName { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
    }
}
