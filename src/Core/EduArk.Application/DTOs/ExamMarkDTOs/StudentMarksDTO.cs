namespace EduArk.Application.DTOs.ExamMarkDTOs
{
    public class StudentMarksDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal Marks { get; set; }
        public string? Grade { get; set; }


        public string? StudentName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ProfileImage { get; set; }
    }
}
