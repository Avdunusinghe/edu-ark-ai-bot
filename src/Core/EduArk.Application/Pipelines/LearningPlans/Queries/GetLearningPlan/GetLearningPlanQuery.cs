using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.LearningPlans.Queries.GetLearningPlan
{
    public record GetAllLearningPlanQuery : IRequest<List<LearningPlanDetailsDTO>>
    {
    }

    public class GetLearningPlanQueryHandler
        : IRequestHandler<GetAllLearningPlanQuery, List<LearningPlanDetailsDTO>>
    {

        private readonly ILearningPlanQueryRepository _learningPlanQueryRepository;
        private readonly ILearningPlanCommandRepository _leaningPlanCommandRepository;

        public GetLearningPlanQueryHandler
            (ILearningPlanQueryRepository learningPlanQueryRepository,
            ILearningPlanCommandRepository leaningPlanCommandRepository)
        {
            _learningPlanQueryRepository = learningPlanQueryRepository;
            _leaningPlanCommandRepository = leaningPlanCommandRepository;
        }

        public async Task<List<LearningPlanDetailsDTO>> Handle(GetAllLearningPlanQuery rewuest, CancellationToken cancellationToken)
        {
            List<LearningPlanDetailsDTO> learningPlanData = new List<LearningPlanDetailsDTO>();

            List<LearningPlan> learningPlans = await _learningPlanQueryRepository.GetAll(cancellationToken);

            learningPlanData = learningPlans
                .Where(lp => lp.IsActive != false)
                .Select(lp => new LearningPlanDetailsDTO
            {
                Id = lp.Id,
                StudentName = lp.StudentName,
                SchoolName = lp.SchoolName,
                SchoolGrade = lp.SchoolGrade,
                StudentMark = lp.StudentMark,
                AverageMark = lp.AverageMark,
                LearningPattern = lp.LearningPattern

            }).ToList();

            return learningPlanData;
        }
    }

}
