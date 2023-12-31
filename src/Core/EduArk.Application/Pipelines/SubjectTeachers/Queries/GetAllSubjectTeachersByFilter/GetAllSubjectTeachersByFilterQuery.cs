using EduArk.Application.DTOs.SubjectTeachersDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Application.Pipelines.Users.Queries.GetUserDetailMasterDataByFilter;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.SubjectTeachers.Queries.GetAllSubjectTeachersByFilter
{
    public record GetAllSubjectTeachersByFilterQuery(SubjectTeacherFilterDTO filter) : IRequest<List<SubjectTeachersDTO>>
    {
    }

    public class GetAllSubjectTeachersByFilterQueryHandler : IRequestHandler<GetAllSubjectTeachersByFilterQuery, List<SubjectTeachersDTO>>
    {
        private readonly IMediator _mediator;
        private readonly ISubjectAcademicLevelQueryRepository _subjectAcademicLevelQueryRepository;
       

        public GetAllSubjectTeachersByFilterQueryHandler(IMediator mediator, ISubjectAcademicLevelQueryRepository subjectAcademicLevelQueryRepository)
        {
            this._mediator = mediator;
            this._subjectAcademicLevelQueryRepository = subjectAcademicLevelQueryRepository;
           
        }
        public async Task<List<SubjectTeachersDTO>> Handle(GetAllSubjectTeachersByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjectTeachers = new List<SubjectTeachersDTO>();

                var academicLevelSubjects = await _subjectAcademicLevelQueryRepository
                                            .Query(x => x.AcademicLevelId == request.filter.AcademicLevelId);
                                           
                                            

                if(!string.IsNullOrEmpty(request.filter.SubjectName))
                {
                    academicLevelSubjects.Where(x => x.Subject.Name.Contains(request.filter.SubjectName));
                }

                var listOfSubjects = academicLevelSubjects.OrderBy(x=>x.Subject.Name).ToList();

                var filter = new UserDetailsMasterDataFilterDTO()
                {
                    Name = string.Empty,
                    RoleId = (int)RoleType.Teacher
                };

                var listOfTeachers = await _mediator
                                    .Send(new GetUserDetailMasterDataByFilterQuery(filter), cancellationToken);

                foreach (var item in listOfSubjects)
                {
                    var subjectTeacherData = new SubjectTeachersDTO();
                    subjectTeacherData.Subject = item.Subject.Name;
                    subjectTeacherData.SubjectId = item.Subject.Id;
                    subjectTeacherData.AcademicLevelId = item.AcademicLevelId;
                    subjectTeacherData.AcademicYearId = request.filter.AcademicYearId;

                    var assignedSubjectTeachers = item.Subject.SubjectTeachers
                                                  .Where(x => x.AcademicYearId == request.filter.AcademicYearId)
                                                  .ToList();

                    subjectTeacherData.AssignedTeacherIds = assignedSubjectTeachers
                                                           .Select(x => x.TeacherId)
                                                           .ToList();

                    subjectTeacherData.AssignedSubjectTeachersName = string.Join(",", assignedSubjectTeachers
                                                                    .Select(x => x.Teacher.FirstName)
                                                                    .ToList());

                    subjectTeacherData.AssignedTeachersCount = assignedSubjectTeachers.Count;
                    subjectTeacherData.AllTeachers = listOfTeachers;

                    subjectTeachers.Add(subjectTeacherData);
                }

                return subjectTeachers;
            }
            catch (Exception ex)
            {

                return new List<SubjectTeachersDTO>();
            }
        }
    }
}
