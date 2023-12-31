using EduArk.Domain.Enums;

namespace EduArk.Application.DTOs.ClassDTOs
{
    public class ClassDTO
    {
        public ClassDTO()
        {
            ClassSubjectTeachers = new List<ClassSubjectTeacherDTO>();
        }

        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int ClassNameId { get; set; }
        public string Name { get; set; }

        public ClassCategory ClassCategoryId { get; set; }
        public LanguageStream LanguageStreamId { get; set; }
        public int ClassTeacherId { get; set; }

        public List<ClassSubjectTeacherDTO> ClassSubjectTeachers { get; set; }
    }
}
