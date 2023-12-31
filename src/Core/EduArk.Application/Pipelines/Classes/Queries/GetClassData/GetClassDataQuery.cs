using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Classes.Queries.GetClassById
{
    public record GetClassDataQuery(int academicYearId, int academicLevelId, int classNameId) : IRequest<ClassDTO>
    {

    }

    public class GetClassDataQueryHandler : IRequestHandler<GetClassDataQuery, ClassDTO>
    {
        private readonly IClassQueryRepository _classQueryRepository;
        private readonly ISubjectAcademicLevelQueryRepository _subjectAcademicLevelQueryRepository;

        public GetClassDataQueryHandler(IClassQueryRepository classQueryRepository, ISubjectAcademicLevelQueryRepository subjectAcademicLevelQueryRepository)
        {
            this._classQueryRepository = classQueryRepository;
            this._subjectAcademicLevelQueryRepository = subjectAcademicLevelQueryRepository;
        }

        public async Task<ClassDTO> Handle(GetClassDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var  classDatails = (await _classQueryRepository
                                    .Query(x=>x.AcademicYearId == request.academicYearId && 
                                    x.AcademicLevelId == request.academicLevelId && 
                                    x.ClassNameId == request.classNameId))
                                    .FirstOrDefault();

                var classTeacher = classDatails.ClassTeachers
                                   .FirstOrDefault(x => x.IsPrimary == true);

                var classData = new ClassDTO()
                {
                    AcademicLevelId = classDatails.AcademicLevelId,
                    AcademicYearId = classDatails.AcademicYearId,
                    ClassCategoryId = classDatails.ClassCategory,
                    ClassNameId = classDatails.ClassNameId,
                    ClassTeacherId = classTeacher != null ? classTeacher.TeacherId : 0,
                    LanguageStreamId = classDatails.LanguageStream,
                    Name = classDatails.Name

                };

                var academicLevelSubjects = (await _subjectAcademicLevelQueryRepository
                                           .Query(x => x.AcademicLevelId == classDatails.AcademicLevelId))
                                           .OrderBy(s => s.Subject.Name)
                                           .ToList();

                foreach(var item in academicLevelSubjects)
                {
                    var allSubjectTeachers = item.Subject.SubjectTeachers
                                            .Where(x => x.AcademicYearId == request.academicYearId && x.IsActive == true)
                                            .Select(t => new DropDownDTO()
                                            {
                                                Id = t.Id,
                                                Name = t.Teacher.FirstName

                                            }).ToList();

                    var subjectTeacher = classDatails.ClassSubjectTeachers
                                        .FirstOrDefault(x => x.SubjectId == item.SubjectId);

                    var classSubjectTeacherDto = new ClassSubjectTeacherDTO()
                    {
                        Id = subjectTeacher != null ? subjectTeacher.Id : 0,
                        AcademicLevelId = request.academicLevelId,
                        AcademicYearId = request.academicYearId,
                        AllSubjectTeachers = allSubjectTeachers,
                        SubjectId = item.SubjectId,
                        SubjectName = item.Subject.Name,
                        SubjectTeacherId = subjectTeacher != null ? subjectTeacher.SubjectTeacherId : 0
                    };

                    classData.ClassSubjectTeachers.Add(classSubjectTeacherDto);
                }

                return classData;

            }
            catch (Exception ex)
            {
                return new ClassDTO();
            }
        }
    }
}
