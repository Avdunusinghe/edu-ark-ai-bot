using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Enums;

namespace EduArk.Application.DTOs.TenantDTOs
{
    public class AssessmentsDTO
    {
        public AssessmentsDTO() { }

        public int Id { get; set; }
        public string AssessmentName { get; set; }
        public int SubjectId { get; set; }
        public int LessonId { get; set; }
        public AssessmentType AssessmentType { get; set; }
        public virtual Topic Topic { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

    }
}
