using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Assessments.Commands.GetByIdAssessments
{

    public record GetByIdAssessmentQuery(int id) : IRequest<AssessmentsDTO>;
    public class GetByIdAssessmentsQueryHandler : IRequestHandler<GetByIdAssessmentQuery, AssessmentsDTO>
    {
        private readonly IAssessmentQueryRepository _assessmentQueryRepository;
        private readonly IAssessmentCommandRepository _assessmentCommandRepository;

        public GetByIdAssessmentsQueryHandler(IAssessmentQueryRepository assessmentQueryRepository, IAssessmentCommandRepository assessmentCommandRepository)
        {
            this._assessmentQueryRepository = assessmentQueryRepository;
            this._assessmentCommandRepository = assessmentCommandRepository;
        }
        public async Task<AssessmentsDTO> Handle(GetByIdAssessmentQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var assessment = await _assessmentQueryRepository.GetById(request.id, cancellationToken);

                if (assessment == null)
                {
                    return new AssessmentsDTO();
                }
                else
                {
                    var assesmentData = new AssessmentsDTO
                    {
                        Id = assessment.Id,
                        AssessmentName = assessment.AssessmentName,
                        //SubjectId = assessment.SubjectId,
                        //LessonId = assessment.LessonId,
                        AssessmentType = assessment.AssessmentType,
                        //Topic = assessment.Topic,

                    };

                    return assesmentData;



                }

            }
            catch (Exception ex)
            {
                return new AssessmentsDTO();
            }
        }


    }

}
