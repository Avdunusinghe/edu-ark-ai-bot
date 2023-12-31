using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Lessons.Queries.UpdateLesson
{
    public record UpdateLessonQuery(LessonDTO LessonDetails) : IRequest<ResultDTO>;

    public class UpdateLessonQueryHandler : IRequestHandler<UpdateLessonQuery, ResultDTO>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;

        public UpdateLessonQueryHandler(ILessonQueryRepository lessonQueryRepository, ILessonCommandRepository lessonCommandRepository)
        {
            _lessonQueryRepository = lessonQueryRepository;
            _lessonCommandRepository = lessonCommandRepository;
        }

        public async Task<ResultDTO> Handle(UpdateLessonQuery requst, CancellationToken cancellationToken)
        {
            try
            {
                var lesson = await _lessonQueryRepository.GetById(requst.LessonDetails.Id, cancellationToken);

                if (lesson == null) 
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.LESSON_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE
                    });
                }

                UpdateLessonFromDTO(requst.LessonDetails, lesson);

                await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                return ResultDTO.Success
                    (
                        ApplicationResponseConstant.LESSON_UPDATE_SUCCESS_RESPONSE_MESSAGE
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

        private void UpdateLessonFromDTO(LessonDTO source, Lesson target)
        {
            target.Id = source.Id;
            target.LessonName = source.LessonName;
            target.LessonDescription = source.LessonDescription;
            target.LessonGrade = source.LessonGrade;
            target.LessonStatus = source.LessonStatus;
            target.LessonSubject = source.LessonSubject;
            
        }
    }
}
