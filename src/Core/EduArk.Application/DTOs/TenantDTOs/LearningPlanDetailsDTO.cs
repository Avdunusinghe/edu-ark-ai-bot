namespace EduArk.Application.DTOs.TenantDTOs
{
    public class LearningPlanDetailsDTO
    {
        public LearningPlanDetailsDTO() { }

        public int Id { get; set; }
        public string StudentName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolGrade { get; set; }
        public string StudentMark { get; set; }
        public string AverageMark { get; set; }
        public string LearningPattern { get; set; }

        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
