using EduArk.Application.Common.Constants;
using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.SubjectDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Subjects.Commands.SaveSubject
{
    public record SaveSubjectCommand(SubjectDTO subject) : IRequest<ResultDTO>
    {
    }

    public class SaveSubjectCommandHandler : IRequestHandler<SaveSubjectCommand, ResultDTO>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;
        private readonly ISubjectCommandRepository _subjectCommandRepository;

        public SaveSubjectCommandHandler(ISubjectQueryRepository _subjectQueryRepository, ISubjectCommandRepository _subjectCommandRepository)
        {
            this._subjectQueryRepository = _subjectQueryRepository;
            this._subjectCommandRepository = _subjectCommandRepository;
        }
        public async Task<ResultDTO> Handle(SaveSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subject = await _subjectQueryRepository.GetById(request.subject.Id, cancellationToken);

                if(subject is null)
                {
                    subject = request.subject.ToEntity();

                    AddNewSubjectAcademicLevels(subject, request.subject.SubjectAcademicLevels);

                    var newSubject = await _subjectCommandRepository.AddAsync(subject, cancellationToken);

                    return ResultDTO
                          .Success(ApplicationResponseConstant.SUBJECT_SAVE_SUCCESS_RESPONSE_MESSAGE, newSubject.Id);
                }
                else
                {
                    subject = request.subject.ToEntity(subject);

                     var exsistingSubjectAcademicIds = subject.SubjectAcademicLevels
                                                      .Select(x=>x.AcademicLevelId)
                                                      .ToList();

                    var selectedSubjectAcademicLevels = request.subject.SubjectAcademicLevels
                                                       .ToList();

                    var newSubjectAcademicLevels = selectedSubjectAcademicLevels
                                                   .Except(exsistingSubjectAcademicIds)
                                                   .ToList();

                    var deletedSubjectAcademicLevel = exsistingSubjectAcademicIds
                                                     .Except(selectedSubjectAcademicLevels)
                                                     .ToList();

                    AddNewSubjectAcademicLevels(subject, newSubjectAcademicLevels);

                    foreach(var subjectAcademicLevel in subject.SubjectAcademicLevels
                                                        .Where(x=> deletedSubjectAcademicLevel
                                                        .Contains(x.AcademicLevelId))
                                                        .ToList())
                    {
                        subject.SubjectAcademicLevels.Remove(subjectAcademicLevel);
                    }

                    await _subjectCommandRepository.UpdateAsync(subject, cancellationToken);

                    return ResultDTO
                         .Success(ApplicationResponseConstant.SUBJECT_UPDATE_SUCCESS_RESPONSE_MESSAGE, subject.Id);
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

        private void AddNewSubjectAcademicLevels(Subject subject, List<int> subjectAcademicLevelIds)
        {
            foreach(var subjectAcademicLevelId in subjectAcademicLevelIds)
            {
                subject.SubjectAcademicLevels.Add(new SubjectAcademicLevel()
                {
                    Subject = subject,
                    AcademicLevelId = subjectAcademicLevelId
                });
            }
        }
    }
}
