using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.ClassDTOs
{
    public class ClassSubjectTeacherDTO
    {
        public ClassSubjectTeacherDTO()
        {
            AllSubjectTeachers = new List<DropDownDTO>();
        }
        public int Id { get; set; }
        public int ClassNameId { get; set; }
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public int SubjectId { get; set; }
        public int SubjectTeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }


        public string SubjectName { get; set; }
        public List<DropDownDTO> AllSubjectTeachers { get; set; }

    }
}
