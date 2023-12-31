namespace EduArk.Application.DTOs.ExamMarkDTOs
{
    public class StudentSubjectMarkDTO
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int AcademicLevelId { get; set; }
        public decimal Marks { get; set; }
        public string Grade { get; set; }

        public string? StudentName { get; set; }
    }
}
