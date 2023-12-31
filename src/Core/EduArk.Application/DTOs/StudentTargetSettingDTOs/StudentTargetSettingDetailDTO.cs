namespace EduArk.Application.DTOs.StudentTargetSettingDTOs
{
    public class StudentTargetSettingDetailDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string?StudentProfileImage{ get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public decimal PredictedMark { get; set; }
        public decimal? TeacherTaergetScore { get; set; }
        public string? Grade { get; set; }
        public string? Severity { get; set; }

    }
}
