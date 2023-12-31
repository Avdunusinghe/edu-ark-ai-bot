using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Lessons.Queries.GetLesson
{
    public record GetAllLessonQuery : IRequest<List<LessonDTO>>
    { }

    public class GetAllLessonQueryHandler 
        : IRequestHandler<GetAllLessonQuery, List<LessonDTO>>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;

        public GetAllLessonQueryHandler(ILessonQueryRepository lessonQueryRepository, ILessonCommandRepository lessonCommandRepository)
        {
            _lessonQueryRepository = lessonQueryRepository;
            _lessonCommandRepository = lessonCommandRepository;
        }

        public async Task<List<LessonDTO>> Handle(GetAllLessonQuery request, CancellationToken cancellationToken)
        {
            var lessonData = new List<LessonDTO>();

            var lessons = await _lessonQueryRepository.GetAll(cancellationToken);
            
            lessonData = lessons
                .Where(les => les.IsActive != false)
                .Select(les => new LessonDTO
            {
                Id = les.Id,
                LessonName = les.LessonName,
                LessonDescription = les.LessonDescription,
                LessonGrade = les.LessonGrade,
                LessonStatus = les.LessonStatus,
                LessonSubject = les.LessonSubject,
                IsActive = les.IsActive,

            }).ToList();

            return lessonData;
        }
    }
}
