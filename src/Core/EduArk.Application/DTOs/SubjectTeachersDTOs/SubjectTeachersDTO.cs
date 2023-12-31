using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.SubjectTeachersDTOs
{
    public class SubjectTeachersDTO
    {
        public SubjectTeachersDTO()
        {
            AssignedTeacherIds = new List<int>();
            AllTeachers = new List<DropDownDTO>();
        }
        public int Id { get; set; }
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public int SubjectId { get; set; }
        public string? Subject { get; set; }
        public string? AssignedSubjectTeachersName { get; set; }
        public List<int> AssignedTeacherIds { get; set; }
        public int? AssignedTeachersCount { get; set; }

        public List<DropDownDTO>? AllTeachers { get; set; }
    }
}
