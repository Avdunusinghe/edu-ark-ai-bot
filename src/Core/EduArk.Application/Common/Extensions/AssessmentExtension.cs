using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;

namespace EduArk.Application.Common.Extensions
{
    public static class AssessmentExtension
    {
        public static Assessment ToEntity(this AssessmentsDTO dto, Assessment? assessment = null)
        {
            if (assessment == null)
            {
                assessment = new Assessment();
            }

            assessment.AssessmentName = dto.AssessmentName;
            //assessment.SubjectId = dto.SubjectId;
            //assessment.LessonId = dto.LessonId;
            assessment.AssessmentType = dto.AssessmentType;
            //assessment.Topic = dto.Topic;

            return assessment;

        }
    }

    
}

