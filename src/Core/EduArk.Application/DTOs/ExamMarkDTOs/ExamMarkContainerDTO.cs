namespace EduArk.Application.DTOs.ExamMarkDTOs
{
    public class ExamMarkContainerDTO
    {
        public ExamMarkContainerDTO()
        {
            StudentMarks = new List<StudentMarksDTO>();
        }
        public int SubjectId { get; set; }
        public int ExamId { get; set; }
        public int AcademicLevelId { get; set; }
        public List<StudentMarksDTO> StudentMarks { get; set; }

    }

    
}
