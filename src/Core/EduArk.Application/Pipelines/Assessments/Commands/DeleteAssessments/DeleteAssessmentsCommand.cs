using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Assessments.Commands.DeleteAssessments
{
    public record DeleteAssessmentsCommand(int id) : IRequest<ResultDTO>;
    public class DeleteAssessmentsCommandHandler : IRequestHandler<DeleteAssessmentsCommand, ResultDTO>   
    {
        private readonly IAssessmentQueryRepository _assessmentQueryRepository;
        private readonly IAssessmentCommandRepository _assessmentCommandRepository;

        /// <summary>
        /// Constructor for DeleteLearningPlanCommandHandler
        /// </summary>
        /// <param name="assessmentsQueryRepository">Learning Plan query repository</param>
        /// <param name="leaningPlanCommandRepository">Learning Plan command repository</param>

        public DeleteAssessmentsCommandHandler(IAssessmentQueryRepository assessmentQueryRepository, IAssessmentCommandRepository assessmentCommandRepository)
        {
            this._assessmentQueryRepository = assessmentQueryRepository;
            this._assessmentCommandRepository = assessmentCommandRepository;
        }

        /// <summary>
        /// Executes the command to delete a learning plan by setting their IsActive property to false.
        /// </summary>
        /// <param name="request">The command to delete a learning plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the command.</returns>

        public async Task<ResultDTO> Handle(DeleteAssessmentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var assessments = await _assessmentQueryRepository.GetById(request.id, cancellationToken);

                if (assessments == null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.ASSESSMENTS_SAVE_SUCCESS_RESPONSE_MESSAGE
                    });
                }
                else
                {
                    assessments.IsActive = false;

                    await _assessmentCommandRepository.UpdateAsync(assessments, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.ASSESSMENTS_DELETE_SUCCESS_RESPONSE_MEESSAGE);
                }

            }
            catch (Exception ex)
            {
                return ResultDTO.Failure(new List<string>
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE
                });
            }
        }
    }
}
