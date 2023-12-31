using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.LearningPlans.Queries.UpdateLearningPlan
{
    public record UpdateLearningPlanQuery(LearningPlanDetailsDTO LearningPlanDetails) : IRequest<ResultDTO>;

    public class UpdateLearningPlanCommandHandler : IRequestHandler<UpdateLearningPlanQuery, ResultDTO>
    {
        private readonly ILearningPlanCommandRepository _learningPlanCommandRepository;
        private readonly ILearningPlanQueryRepository _learningPlanQueryRepository;

        public UpdateLearningPlanCommandHandler(ILearningPlanQueryRepository learningPlanQueryRepository, ILearningPlanCommandRepository learningPlanCommandRepository)
        {
            _learningPlanCommandRepository = learningPlanCommandRepository;
            _learningPlanQueryRepository = learningPlanQueryRepository;
        }

        public async Task<ResultDTO> Handle(UpdateLearningPlanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var learningPlan = await _learningPlanQueryRepository.GetById(request.LearningPlanDetails.Id, cancellationToken);

                if (learningPlan == null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.LEARNING_PLAN_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE
                    });
                }

                UpdateLearningPlanFromDTO(request.LearningPlanDetails, learningPlan);

                await _learningPlanCommandRepository.UpdateAsync(learningPlan, cancellationToken);

                return ResultDTO.Success(
                    ApplicationResponseConstant.LEARNIG_PLAN_UPDATE_SUCCESS_RESPONSE_MESSAGE
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

        private void UpdateLearningPlanFromDTO(LearningPlanDetailsDTO source, LearningPlan target)
        {
            target.Id = source.Id;
            target.SchoolName = source.SchoolName;
            target.StudentName = source.StudentName;
            target.SchoolGrade = source.SchoolGrade;
            target.StudentMark = source.StudentMark;
            target.AverageMark = source.AverageMark;
            target.LearningPattern = source.LearningPattern;
        }
    }
}
