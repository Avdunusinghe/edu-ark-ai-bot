namespace EduArk.Application.DTOs.ClassDTOs
{
    public class ClassDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int ClassNameId { get; set; }
        public string ClassTeacherName { get; set; }
        public int TotalStudentCount { get; set; }
    }
}
