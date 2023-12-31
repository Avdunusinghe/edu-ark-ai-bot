namespace EduArk.Application.DTOs.TenantDTOs
{
    public class LessonDTO
    {
        public LessonDTO() { }

        public int Id { get; set; }
        public string? LessonName { get; set; }
        public string? LessonDescription { get; set; }
        public string? LessonGrade { get; set; }
        public string? LessonSubject { get; set; }
        public string? LessonStatus { get; set; }

        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
