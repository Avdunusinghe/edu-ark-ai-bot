using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Assessments.Queries.UpdateAssessments
{
    public record UpdateAssessmentsCommand(AssessmentsDTO Assessments) : IRequest<ResultDTO>;

    public class UpdateAssessmentsQueryHandler : IRequestHandler<UpdateAssessmentsCommand, ResultDTO>
    {
        private readonly IAssessmentQueryRepository _assessmentQueryRepository;
        private readonly IAssessmentCommandRepository _assessmentCommandRepository;

        public UpdateAssessmentsQueryHandler(IAssessmentQueryRepository assessmentQueryRepository, IAssessmentCommandRepository assessmentCommandRepository)
        {
            this._assessmentQueryRepository = assessmentQueryRepository;
            this._assessmentCommandRepository = assessmentCommandRepository;
        }

        public async Task<ResultDTO> Handle(UpdateAssessmentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var assessments = await _assessmentQueryRepository.GetById(request.Assessments.Id, cancellationToken);

                if (assessments == null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.ASSESSMENTS_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE
                    });
                }

                UpdateAssessmentsFromDTO(request.Assessments, assessments);

                await _assessmentCommandRepository.UpdateAsync(assessments, cancellationToken);

                return ResultDTO.Success(
                    ApplicationResponseConstant.ASSESSMENTS_UPDATE_SUCCESS_RESPONSE_MESSAGE
                    );
            }
            catch (Exception ex)
            {
                return ResultDTO.Failure(new List<string>
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE
                });
            }
        }

        private void UpdateAssessmentsFromDTO(AssessmentsDTO source, Assessment target)
        {
            target.Id = source.Id;
            target.AssessmentName = source.AssessmentName;
            //target.SubjectId = source.SubjectId;
            //target.LessonId = source.LessonId;
            target.AssessmentType = source.AssessmentType;
            //target.Topic = source.Topic;
        }
    }
}
