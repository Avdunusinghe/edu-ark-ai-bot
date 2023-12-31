using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.SubjectTeachersDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.SubjectTeachers.Commands.SaveSubjectTeachers
{
    public record SaveSubjectTeachersCommand(SubjectTeachersDTO subjectTeachersDetail) : IRequest<ResultDTO>
    {
    }

    public class SaveSubjectTeachersCommandHandler : IRequestHandler<SaveSubjectTeachersCommand, ResultDTO>
    {
        private readonly ISubjectTeacherQueryRepository _subjectTeacherQueryRepository;
        private readonly ISubjectTeacherCommandRepository _subjectTeacherCommandRepository;
        public SaveSubjectTeachersCommandHandler(ISubjectTeacherQueryRepository subjectTeacherQueryRepository, ISubjectTeacherCommandRepository subjectTeacherCommandRepository)
        {
            this._subjectTeacherQueryRepository = subjectTeacherQueryRepository;
            this._subjectTeacherCommandRepository = subjectTeacherCommandRepository;
        }
        public async Task<ResultDTO> Handle(SaveSubjectTeachersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var listOfExsistingTeachers = (await _subjectTeacherQueryRepository
                                            .Query(x => x.SubjectId == request.subjectTeachersDetail.SubjectId &&
                                            x.AcademicYearId == request.subjectTeachersDetail.AcademicYearId &&
                                            x.AcademicLevelId == request.subjectTeachersDetail.AcademicLevelId &&
                                            x.EndDate.HasValue == false)).ToList();

                var newlyAddedTecahersId = (from t in request.subjectTeachersDetail.AssignedTeacherIds where 
                                            !listOfExsistingTeachers.Any(s => s.TeacherId == t) select t);

                foreach (var teacherId in newlyAddedTecahersId)
                {
                    var subjectTeacher = new SubjectTeacher()
                    {
                        AcademicLevelId = request.subjectTeachersDetail.AcademicLevelId,
                        AcademicYearId = request.subjectTeachersDetail.AcademicYearId,
                        SubjectId = request.subjectTeachersDetail.SubjectId,
                        TeacherId = teacherId,
                        StartDate = DateTime.UtcNow,
                        IsActive = true,
                       
                    };

                    await _subjectTeacherCommandRepository.AddAsync(subjectTeacher, cancellationToken);
                }

                var deletedTeachers = (from d in listOfExsistingTeachers where 
                                       request.subjectTeachersDetail.AssignedTeacherIds.Any(x => x == d.TeacherId) select d)
                                       .ToList();

                foreach (var subjectTeacher in deletedTeachers)
                {
                    subjectTeacher.EndDate = DateTime.UtcNow;
                    subjectTeacher.IsActive = false;

                    await _subjectTeacherCommandRepository.UpdateAsync(subjectTeacher, cancellationToken);
                }

                return ResultDTO.Success(ApplicationResponseConstant.SUBJECT_TEACHER_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE);
            }
            catch (Exception)
            {
                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE,
                });
            }
        }
    }
}
