namespace EduArk.Application.DTOs.UserDTOs
{
    public class StudentExcelContainerDTO
    {
        public StudentExcelContainerDTO()
        {
            Students = new List<StudentDTO>();
        }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int ClassNameId { get; set; }
        public int ClassTeacherId { get; set; }

        public List<StudentDTO> Students { get; set; }
    }
}
