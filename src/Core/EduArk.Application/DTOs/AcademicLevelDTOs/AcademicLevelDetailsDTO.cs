namespace EduArk.Application.DTOs.AcademicLevelDTOs
{
    public class AcademicLevelDetailsDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LevelHeadId { get; set; }
        public string LevelHeadName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
