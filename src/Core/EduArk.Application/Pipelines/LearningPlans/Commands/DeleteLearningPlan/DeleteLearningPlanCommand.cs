using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.LearningPlans.Commands.DeleteLearningPlan
{
    public record DeleteLearningPlanCommand(int Id) : IRequest<ResultDTO>;
    public class DeleteLearningPlanCommandHandler : IRequestHandler<DeleteLearningPlanCommand, ResultDTO>
    {
        private readonly ILearningPlanQueryRepository _learningPlanQueryRepository;
        private readonly ILearningPlanCommandRepository _learningPlanCommanRepository;

        /// <summary>
        /// Constructor for DeleteLearningPlanCommandHandler
        /// </summary>
        /// <param name="learningPlanQueryRepository">Learning Plan query repository</param>
        /// <param name="leaningPlanCommandRepository">Learning Plan command repository</param>

        public DeleteLearningPlanCommandHandler(ILearningPlanQueryRepository learningPlanQueryRepository, ILearningPlanCommandRepository leaningPlanCommandRepository)
        {
            _learningPlanQueryRepository = learningPlanQueryRepository;
            _learningPlanCommanRepository = leaningPlanCommandRepository;
        }

        /// <summary>
        /// Executes the command to delete a learning plan by setting their IsActive property to false.
        /// </summary>
        /// <param name="request">The command to delete a learning plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the command.</returns>

        public async Task<ResultDTO> Handle(DeleteLearningPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var learningPlan = await _learningPlanQueryRepository.GetById(request.Id, cancellationToken);

                if (learningPlan == null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.LEARNING_PLAN_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE
                    });
                }
                else
                {
                    learningPlan.IsActive = false;

                    await _learningPlanCommanRepository.UpdateAsync(learningPlan, cancellationToken);

                    return ResultDTO
                            .Success(
                                ApplicationResponseConstant.LEARNING_PLAN_DELETE_SUCCESS_RESPONSE_MEESSAGE
                            );
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
