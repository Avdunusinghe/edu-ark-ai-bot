using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;

namespace System
{
    public static class LearningPlanExtension
    {
        public static LearningPlan ToEntity(this LearningPlanDetailsDTO dto, LearningPlan? learningPlan = null)
        {
            if (learningPlan == null) 
            {
                learningPlan = new LearningPlan();
            }

            learningPlan.StudentName = dto.StudentName;
            learningPlan.SchoolName = dto.SchoolName;
            learningPlan.SchoolGrade = dto.SchoolGrade;
            learningPlan.StudentMark = dto.StudentMark;
            learningPlan.AverageMark = dto.AverageMark;
            learningPlan.LearningPattern = dto.LearningPattern;

            return learningPlan;

        }
    }
}
