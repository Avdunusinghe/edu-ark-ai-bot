using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.StudentTargetSettingDTOs
{
    public class StudentTargetSettingsFilterDTO : CorePaginationFilterDTO
    {
        public string SearchText { get; set; }
        public int ClassNameId { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int SubjectId { get; set; }
        public int SemesterId { get; set; }
        


    }
}
