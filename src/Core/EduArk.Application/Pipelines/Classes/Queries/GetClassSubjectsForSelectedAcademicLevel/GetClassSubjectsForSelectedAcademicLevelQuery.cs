using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Classes.Queries.GetClassSubjectsForSelectedAcademicLevel
{
    public record GetClassSubjectsForSelectedAcademicLevelQuery : IRequest<List<ClassSubjectTeacherDTO>>
    {
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
    }

    public class GetClassSubjectsForSelectedAcademicLevelQueryHandler : IRequestHandler<GetClassSubjectsForSelectedAcademicLevelQuery, List<ClassSubjectTeacherDTO>>
    {
        private readonly ISubjectAcademicLevelQueryRepository _subjectAcademicLevelQueryRepository;

        public GetClassSubjectsForSelectedAcademicLevelQueryHandler(ISubjectAcademicLevelQueryRepository subjectAcademicLevelQueryRepository)
        {
            this._subjectAcademicLevelQueryRepository = subjectAcademicLevelQueryRepository;
        }
        public async Task<List<ClassSubjectTeacherDTO>> Handle(GetClassSubjectsForSelectedAcademicLevelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var classSubjectTeacherData = new List<ClassSubjectTeacherDTO>();

                var academicLevelSubjects = (await _subjectAcademicLevelQueryRepository
                                            .Query(x=>x.AcademicLevelId == request.AcademicLevelId))
                                            .OrderBy(x=>x.Subject.Name)
                                            .ToList();

                foreach (var item in academicLevelSubjects)
                {
                    var allSubjectTeachers = item.Subject.SubjectTeachers
                                              .Where(x => x.AcademicYearId == request.AcademicYearId && x.IsActive == true)
                                              .Select(t => new DropDownDTO() { Id = t.Id, Name = t.Teacher.FirstName })
                                              .ToList();

                    var classSubjectTeacherDto = new ClassSubjectTeacherDTO()
                    {
                        AcademicLevelId = request.AcademicLevelId,
                        AcademicYearId = request.AcademicYearId,
                        AllSubjectTeachers = allSubjectTeachers,
                        SubjectId = item.SubjectId,
                        SubjectName = item.Subject.Name
                    };

                    classSubjectTeacherData.Add(classSubjectTeacherDto);
                }

                return classSubjectTeacherData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
