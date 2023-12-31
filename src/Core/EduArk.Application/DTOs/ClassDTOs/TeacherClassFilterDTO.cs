using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Enums;

namespace EduArk.Application.DTOs.ClassDTOs
{
    public class TeacherClassFilterDTO : CorePaginationFilterDTO
    {
        public string Name { get; set; }
        public int ClassNameId   { get; set; }
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public ClassCategory ClassCategoryId { get; set; }
        public LanguageStream LanguageStreamId { get; set; }
        public int? SubjectId { get; set; }
        public bool ShowMySubjectClasses { get; set; }

    }
}
