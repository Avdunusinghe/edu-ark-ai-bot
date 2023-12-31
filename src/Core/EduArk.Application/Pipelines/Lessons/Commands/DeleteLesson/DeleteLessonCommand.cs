using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Lessons.Commands.DeleteLesson
{
    public record DeleteLessonCommand(int Id) : IRequest<ResultDTO>;
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, ResultDTO>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;

        /// <summary>
        /// Constructor for DeleteLearningPlanCommandHandler
        /// </summary>
        /// <param name="lessonQueryRepository">Learning Plan query repository</param>
        /// <param name="lessonCommandRepository">Learning Plan command repository</param>

        public DeleteLessonCommandHandler(ILessonQueryRepository lessonQueryRepository, ILessonCommandRepository lessonCommandRepository)
        {
            _lessonQueryRepository = lessonQueryRepository;
            _lessonCommandRepository = lessonCommandRepository;
        }

        /// <summary>
        /// Executes the command to delete a learning plan by setting their IsActive property to false.
        /// </summary>
        /// <param name="request">The command to delete a learning plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the command.</returns>

        public async Task<ResultDTO> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lesson = await _lessonQueryRepository.GetById(request.Id, cancellationToken);

                if (lesson == null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.LESSON_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE
                    });
                }
                else
                {
                    lesson.IsActive = false;

                    await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                    return ResultDTO
                            .Success(
                                ApplicationResponseConstant.LESSON_DELETE_SUCCESS_RESPONSE_MEESSAGE
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
