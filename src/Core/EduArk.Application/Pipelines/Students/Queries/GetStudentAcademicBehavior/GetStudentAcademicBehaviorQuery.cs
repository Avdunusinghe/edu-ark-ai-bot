using EduArk.Application.DTOs.StudentDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Students.Queries.GetStudentAcademicBehavior
{
    public record GetStudentAcademicBehaviorQuery(int studentId) : IRequest<StudentAcademicBehaviorDTO>;

    public class GetStudentAcademicBehaviorQueryHandler
                                    : IRequestHandler<GetStudentAcademicBehaviorQuery, StudentAcademicBehaviorDTO>
    {

        private readonly IStudentQueryRepository _studentQueryRepository;

        public GetStudentAcademicBehaviorQueryHandler(IStudentQueryRepository studentQueryRepository)
        {
            this._studentQueryRepository = studentQueryRepository;
        }
        public async Task<StudentAcademicBehaviorDTO> Handle(GetStudentAcademicBehaviorQuery request, CancellationToken cancellationToken)
        {
            var studentBehavior = new StudentAcademicBehaviorDTO();

            var student = (await _studentQueryRepository.Query(x => x.User.Id == request.studentId))
                          .FirstOrDefault();

            studentBehavior.StudentId = student.User.Id;
            studentBehavior.TimeManagementSkills = student.TimeManagementSkills;
            studentBehavior.PersonalMotivation = student.PersonalMotivation;
            studentBehavior.StudyEnvironment = student.StudyEnvironment;
            studentBehavior.TeacherInstructorQuality = student.TeacherInstructorQuality;
            studentBehavior.PriorKnowledgeOfTheSubject = student.PriorKnowledgeOfTheSubject;
            studentBehavior.ClassAttendance = student.ClassAttendance;
            studentBehavior.ConfidentAcademicPerformance = student.ConfidentAcademicPerformance * 20;
            studentBehavior.StudyHours = student.StudyHours;

            return studentBehavior;

        }
    }





}