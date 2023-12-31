using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Subjects.Commands.DeleteSubject
{
    public record DeleteSubjectCommand(int id) : IRequest<ResultDTO>
    {
    }

    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, ResultDTO>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;
        private readonly ISubjectCommandRepository _subjectCommandRepository;

        public DeleteSubjectCommandHandler(ISubjectQueryRepository _subjectQueryRepository, ISubjectCommandRepository _subjectCommandRepository)
        {
            this._subjectQueryRepository = _subjectQueryRepository;
            this._subjectCommandRepository = _subjectCommandRepository;
        }
        public async Task<ResultDTO> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subject = await _subjectQueryRepository.GetById(request.id, cancellationToken);

                if (subject is null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.SUBJECT_NOT_AVAILABLE_RESPONSE_MESSAGE,
                    });
                }
                else
                {
                    subject.IsActive = false;

                    await _subjectCommandRepository.UpdateAsync(subject, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.SUBJECT_DELETE_SUCCESS_RESPONSE_MESSAGE);

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
