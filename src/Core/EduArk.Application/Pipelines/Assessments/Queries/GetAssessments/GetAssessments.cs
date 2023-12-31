using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Application.Pipelines.Assessments.Queries.GetAssessments
{
    public record GetAllAssessmentsQuery : IRequest<List<AssessmentsDTO>>;
    
    public class GetAssessmentsQueryHandler : IRequestHandler<GetAllAssessmentsQuery, List<AssessmentsDTO>>
    {

       
        private readonly IAssessmentQueryRepository _assessmentQueryRepository;
        private readonly IAssessmentCommandRepository _assessmentCommandRepository;

        public GetAssessmentsQueryHandler(IAssessmentQueryRepository assessmentQueryRepository, IAssessmentCommandRepository assessmentCommandRepository)
        {
            this._assessmentQueryRepository = assessmentQueryRepository;
            this._assessmentCommandRepository = assessmentCommandRepository;
        }

        public async Task<List<AssessmentsDTO>> Handle(GetAllAssessmentsQuery rewuest, CancellationToken cancellationToken)
        {
            List<AssessmentsDTO> assessmentData = new List<AssessmentsDTO>();

            List<Assessment> assessments = await _assessmentQueryRepository.GetAll(cancellationToken);

            assessmentData = assessments.Select(assessment  => new AssessmentsDTO
            {
                Id = assessment.Id,
                AssessmentName = assessment.AssessmentName,
                //SubjectId = assessment.SubjectId,
                //LessonId = assessment.LessonId,
                AssessmentType = assessment.AssessmentType,
                //Topic = assessment.Topic,

            }).ToList();

            return assessmentData;
        }
    }
}
