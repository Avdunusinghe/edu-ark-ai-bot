using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Users.Queries.GetStudentsByFilter
{
    public record GetStudentsByFilterQuery(ClassStudentFilterDTO classStudentFilter)
        : IRequest<PaginatedItemDTO<StudentDTO>>
    {
    }

    public class GetStudentsByFilterQueryHandler 
        : IRequestHandler<GetStudentsByFilterQuery, PaginatedItemDTO<StudentDTO>>
    {
        private readonly IStudentQueryRepository _studentQueryRepository;

        public GetStudentsByFilterQueryHandler(IStudentQueryRepository studentQueryRepository)
        {
            this._studentQueryRepository = studentQueryRepository;
        }
        public async Task<PaginatedItemDTO<StudentDTO>> Handle(GetStudentsByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;
                var listOfStudent = await _studentQueryRepository.Query(x => x.IsActive == true);


                listOfStudent = listOfStudent
                    .Where(x => x.StudentClasses
                    .Any(s => s.AcademicYearId == request.classStudentFilter.AcademicYearId &&
                    s.ClassNameId == request.classStudentFilter.ClassNameId &&
                    s.AcademicLevelId == request.classStudentFilter.AcademicLevelId));

                if(!string.IsNullOrEmpty(request.classStudentFilter.Name))
                {
                    listOfStudent = listOfStudent.Where(x=>x.User.FirstName.Contains(request.classStudentFilter.Name));
                }

                totalRecordCount = listOfStudent.Count();

                var studentData = listOfStudent.OrderByDescending(x=>x.User.FirstName)
                                 .Skip(request.classStudentFilter.CurrentPage * request.classStudentFilter.PageSize)
                                 .ToList();

                var availableDataSet = studentData.Select(x=> new StudentDTO()
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    AdmissionNo = x.AdmissionNo,
                    DateOfBirth = x.DateOfBirth,
                    EmegencyContactNo1 = x.EmegencyContactNo1,
                    EmegencyContactNo2 = x.EmegencyContactNo2,

                }).ToList();

                return new PaginatedItemDTO<StudentDTO>
                   (availableDataSet, totalRecordCount, request.classStudentFilter.CurrentPage + 1, request.classStudentFilter.PageSize);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
