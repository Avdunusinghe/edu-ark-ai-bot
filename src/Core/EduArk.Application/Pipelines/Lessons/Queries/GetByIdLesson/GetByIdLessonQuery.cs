using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace EduArk.Application.Pipelines.Lessons.Queries.GetByIdLesson
{
    public record GetByIdLessonQuery(int Id) : IRequest<LessonDTO>;

    public class GetByIdLessonQueryHandler : IRequestHandler<GetByIdLessonQuery, LessonDTO>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;

        public GetByIdLessonQueryHandler(ILessonQueryRepository lessonQueryRepository, ILessonCommandRepository lessonCommandRepository)
        {
            _lessonQueryRepository = lessonQueryRepository;
            _lessonCommandRepository = lessonCommandRepository;
        }

        public async Task<LessonDTO> Handle(GetByIdLessonQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lesson = await _lessonQueryRepository.GetById(request.Id, cancellationToken);

                    var lessonDTO = new LessonDTO
                    {
                        Id = lesson.Id,
                        LessonName = lesson.LessonName,
                        LessonDescription = lesson.LessonDescription,
                        LessonGrade = lesson.LessonGrade,
                        LessonStatus = lesson.LessonStatus,
                        LessonSubject = lesson.LessonSubject,
                    };

                return lessonDTO;
                   
            }
            catch (Exception ex)
            {
                return new LessonDTO();
            }
        }
    }
}
