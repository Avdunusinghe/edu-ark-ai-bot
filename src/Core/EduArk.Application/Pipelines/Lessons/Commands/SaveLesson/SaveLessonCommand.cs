using EduArk.Application.Common.Constants;
using EduArk.Application.Common.Extensions;
using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Lessons.Commands.SaveLesson
{
    public record SaveLessonCommand(LessonDTO LessonDTO) : IRequest<ResultDTO>;
    
    public class SaveLessonCommandHandler : IRequestHandler<SaveLessonCommand, ResultDTO>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ICurrentUserService _currentUserService;

        public SaveLessonCommandHandler
            (ILessonQueryRepository lessonQueryRepository,
            ILessonCommandRepository lessonCommandRepository,
            ICurrentUserService currentUserService)
        {
            _lessonQueryRepository = lessonQueryRepository;
            _lessonCommandRepository = lessonCommandRepository;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Handler methis for SaveLessonCommand
        /// </summary>
        /// <param name="request">SaveLessonCommand</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns>result DTO</returns>

        public async Task<ResultDTO> Handle(SaveLessonCommand request, CancellationToken cancellationToken) 
        {
            try 
            {
                var lesson = await _lessonQueryRepository
                    .GetById(request.LessonDTO.Id, cancellationToken);

                if (lesson == null)
                {
                    lesson = request.LessonDTO.ToEntity();
                    lesson.IsActive = true;
                    await _lessonCommandRepository.AddAsync(lesson, cancellationToken);

                    return ResultDTO
                        .Success(
                           ApplicationResponseConstant.LESSON_SAVE_SUCCESS_RESPONSE_MESSAGE,
                           lesson.Id
                        );
                }
                else
                {
                    lesson = request.LessonDTO.ToEntity(lesson);

                    await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                    return ResultDTO
                        .Success
                        (
                            ApplicationResponseConstant.LESSON_UPDATE_SUCCESS_RESPONSE_MESSAGE,
                            lesson.Id
                        );
                }
            }
            catch (Exception ex)
            {
                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE
                });
            }
        }
    }
}
