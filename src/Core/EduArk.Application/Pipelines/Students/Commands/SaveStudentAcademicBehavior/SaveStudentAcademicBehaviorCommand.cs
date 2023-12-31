using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.StudentDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Students.Commands.SaveStudentAcademicBehavior
{
    public record SaveStudentAcademicBehaviorCommand(StudentAcademicBehaviorDTO studentAcademicBehavior) : IRequest<ResultDTO>;

    public class SaveStudentAcademicBehaviorCommandHandler : IRequestHandler<SaveStudentAcademicBehaviorCommand, ResultDTO>
    {
        private readonly IStudentQueryRepository _studentQueryRepository;
        private readonly IStudentCommandRepository _studentCommandRepository;

        public SaveStudentAcademicBehaviorCommandHandler
        (
            IStudentQueryRepository studentQueryRepository,
            IStudentCommandRepository studentCommandRepository
        )
        {
            this._studentQueryRepository = studentQueryRepository;
            this._studentCommandRepository = studentCommandRepository;
        }
        public async Task<ResultDTO> Handle(SaveStudentAcademicBehaviorCommand request, CancellationToken cancellationToken)
        {
            var student = (await _studentQueryRepository.Query(x=>x.User.Id == request.studentAcademicBehavior.StudentId))
                          .FirstOrDefault();
            var confidentAcademicPerformance = (int)Math
                                               .Ceiling(request.studentAcademicBehavior.ConfidentAcademicPerformance / 20.0);

            student.ConfidentAcademicPerformance = confidentAcademicPerformance;
            student.StudyHours = request.studentAcademicBehavior.StudyHours;
            student.ClassAttendance = request.studentAcademicBehavior.ClassAttendance;
            student.PersonalMotivation = request.studentAcademicBehavior.PersonalMotivation;
            student.PriorKnowledgeOfTheSubject = request.studentAcademicBehavior.PriorKnowledgeOfTheSubject;
            student.StudyEnvironment = request.studentAcademicBehavior.StudyEnvironment;
            student.TeacherInstructorQuality = request.studentAcademicBehavior.TeacherInstructorQuality;
            student.TimeManagementSkills = request.studentAcademicBehavior.TimeManagementSkills;

            await _studentCommandRepository.UpdateAsync(student, cancellationToken);

            return ResultDTO.Success($"Hi {student.User.FirstName} your academic behavior detail updated has been successfully.");
            

        }

        
    }
}
