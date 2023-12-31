using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.LearningPlans.Queries.GetByIdLearningPlan
{
    public record GetByIdLearningPlanQuery(int id) : IRequest<ResultDTO>;
    public class GetByIdLearningPlanQueryHandler : IRequestHandler<GetByIdLearningPlanQuery, ResultDTO>
    {
        private readonly ILearningPlanQueryRepository _learningPlanQueryRepository;
        private readonly ILearningPlanCommandRepository _learningPlanCommandRepository;

        public GetByIdLearningPlanQueryHandler(ILearningPlanQueryRepository learningPlanQueryRepository, ILearningPlanCommandRepository learningPlanCommandRepository)
        {
            _learningPlanQueryRepository = learningPlanQueryRepository;
            _learningPlanCommandRepository = learningPlanCommandRepository;
        }

        public async Task<ResultDTO> Handle(GetByIdLearningPlanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var learningPlan = await _learningPlanQueryRepository.GetById(request.id, cancellationToken);

                if (learningPlan == null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.LEARNING_PLAN_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE
                    });
                }
                else
                {
                    var LearningPlan = new LearningPlanDetailsDTO
                    {
                        Id = learningPlan.Id,
                        StudentName = learningPlan.StudentName,
                        SchoolName = learningPlan.SchoolName,
                        SchoolGrade = learningPlan.SchoolGrade,
                        StudentMark = learningPlan.StudentMark,
                        AverageMark = learningPlan.AverageMark,
                        LearningPattern = learningPlan.LearningPattern,
                    };
                    //return ResultDTO.Success(LearningPlan);
                    return ResultDTO.Success(ApplicationResponseConstant.LEARNING_PLAN_GET_GY_ID_RESPONSE_MESSAGE);

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
