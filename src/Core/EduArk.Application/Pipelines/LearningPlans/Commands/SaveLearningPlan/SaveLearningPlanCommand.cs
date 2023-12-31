using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.LearningPlans.Commands.SaveLearningPlan
{
    public record SaveLearningPlanCommand(LearningPlanDetailsDTO LessonDetails) : IRequest<ResultDTO>;

    public class SaveLessonCommondHandler : IRequestHandler<SaveLearningPlanCommand, ResultDTO>
    {
        private readonly ILearningPlanQueryRepository _learningPlanQueryRepository;
        private readonly ILearningPlanCommandRepository _leaningPlanCommandRepository;


        public SaveLessonCommondHandler
            (ILearningPlanQueryRepository learningPlanQueryRepository,
            ILearningPlanCommandRepository leaningPlanCommandRepository)
        {
            _learningPlanQueryRepository = learningPlanQueryRepository;
            _leaningPlanCommandRepository = leaningPlanCommandRepository;
        }

        /// <summary>
        /// Handler methis for SaveLessonCommand
        /// </summary>
        /// <param name="request">SaveLessonCommand</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns>result DTO</returns>
        public async Task<ResultDTO> Handle(SaveLearningPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var learningPlan = await _learningPlanQueryRepository
                    .GetById(request.LessonDetails.Id, cancellationToken);

                if (learningPlan == null)
                {
                    learningPlan = request.LessonDetails.ToEntity();

                    await _leaningPlanCommandRepository.AddAsync(learningPlan, cancellationToken);

                    return ResultDTO
                        .Success(
                                 ApplicationResponseConstant.LEARNIG_PLAN_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE,
                                 learningPlan.Id
                                 );
                }
                else
                {
                    learningPlan = request.LessonDetails.ToEntity(learningPlan);

                    await _leaningPlanCommandRepository.UpdateAsync(learningPlan, cancellationToken);

                    return ResultDTO
                            .Success
                            (
                                ApplicationResponseConstant.LEARNIG_PLAN_UPDATE_SUCCESS_RESPONSE_MESSAGE,
                                learningPlan.Id
                             );
                }


            }
            catch (Exception ex)
            {
                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE,
                });
            }
        }

    }
}
