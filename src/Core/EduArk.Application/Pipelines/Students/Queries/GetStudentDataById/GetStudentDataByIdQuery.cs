using EduArk.Application.DTOs.StudentDTOs;
using EduArk.Application.Pipelines.Students.Queries.GetStudentAcademicBehavior;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Students.Queries.GetStudentDataById
{
    public record GetStudentDataByIdQuery(int studentId) : IRequest<StudentBasicDetailDTO>
    {
    }

    public class GetStudentDataByIdQueryHandler : IRequestHandler<GetStudentDataByIdQuery, StudentBasicDetailDTO>
    {
        private readonly IStudentQueryRepository _studentQueryRepository;
        private readonly IMediator _mediator;
        public GetStudentDataByIdQueryHandler
        (
            IStudentQueryRepository studentQueryRepository,
            IMediator mediator
        )
        {
            this._studentQueryRepository = studentQueryRepository;
            this._mediator = mediator;
        }
        public async Task<StudentBasicDetailDTO> Handle(GetStudentDataByIdQuery request, CancellationToken cancellationToken)
        {
            var studentData = new StudentBasicDetailDTO();

            var student = (await _studentQueryRepository.Query(x => x.User.Id == request.studentId))
                            .FirstOrDefault();

            studentData.StudentAcademicBehavior = await _mediator.Send(new GetStudentAcademicBehaviorQuery(request.studentId)
                                                    , cancellationToken);
            studentData.FullName = $"{student.User.FirstName} {student.User.LastName}";
            studentData.AdmissionNo = student.AdmissionNo;
            studentData.ProfileUrl = student.User.ProfileImageUrl ?? string.Empty;
            studentData.EmegencyContactNo1 = student.EmegencyContactNo1 ?? string.Empty;
            studentData.EmegencyContactNo2 = student.EmegencyContactNo2 ?? string.Empty;
            studentData.Email = student.User.Email ?? string.Empty;

            return studentData;

        }
    }
}
