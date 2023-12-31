using EduArk.Application.Common.Helper;
using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Classes.Queries.GetTeacherClassesByFilter
{
    public class GetTeacherClassesByFilterQuery : IRequest<PaginatedItemDTO<BasicTeacherClassDTO>>
    {
        public TeacherClassFilterDTO Filter { get; set; }
    }

    public class GetTeacherClassesByFilterQueryHandler : IRequestHandler<GetTeacherClassesByFilterQuery, PaginatedItemDTO<BasicTeacherClassDTO>>
    {
        private readonly IClassQueryRepository _classQueryRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTeacherClassesByFilterQueryHandler(IClassQueryRepository classQueryRepository, ICurrentUserService currentUserService)
        {
            this._classQueryRepository = classQueryRepository;
            this._currentUserService = currentUserService;
        }
        public async Task<PaginatedItemDTO<BasicTeacherClassDTO>> Handle(GetTeacherClassesByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;
                var query = await _classQueryRepository.Query(x => x.IsActive == true);

               

                if (!string.IsNullOrEmpty(request.Filter.Name))
                {
                    query = query.Where(x=>x.Name.Contains(request.Filter.Name));
                }

                if(request.Filter.AcademicYearId > 0)
                {
                    query = query.Where(x=>x.AcademicYearId == request.Filter.AcademicYearId);
                }

                if(request.Filter.AcademicLevelId > 0)
                {
                    query = query.Where(x=>x.AcademicLevelId == request.Filter.AcademicLevelId);
                }

                if(request.Filter.LanguageStreamId > 0)
                {
                    query = query.Where(x=>x.LanguageStream == request.Filter.LanguageStreamId);
                }

                if(request.Filter.ClassCategoryId > 0)
                {
                    query = query.Where(x=>x.ClassCategory == request.Filter.ClassCategoryId);
                }

                if(request.Filter.ShowMySubjectClasses == true && request.Filter.SubjectId > 0)
                {
                    query = query.Where(x => x.ClassSubjectTeachers
                                 .Any(s => s.SubjectTeacher.Teacher.Id == _currentUserService.UserId.Value && s.SubjectId == request.Filter.SubjectId));
                    
                }
                else
                {
                    query = query.Where(x => x.ClassTeachers.Any(a => a.IsActive == true && a.TeacherId == _currentUserService.UserId.Value));
                }

                totalRecordCount = query.Count();

                var listOfAvailableClasses = query.OrderByDescending(x=>x.CreatedDate)
                                            .Skip(request.Filter.CurrentPage * request.Filter.PageSize)
                                            .Take(request.Filter.PageSize)
                                            .ToList();

                var classDetails = listOfAvailableClasses
                                  .Select(classData => new BasicTeacherClassDTO()
                {
                    AcademicLevelId = classData.AcademicLevelId,
                    AcademicYearId = classData.AcademicYearId,
                    ClassCategoryName = EnumHelper.GetEnumDescription(classData.ClassCategory),
                    ClassNameId = classData.ClassNameId,
                    ClassName = classData.ClassName.Name,
                    AcademicLevelName = classData.AcademicLevel.Name,
                    LanguageStreamName = EnumHelper.GetEnumDescription(classData.LanguageStream)

                }).ToList();

                return new PaginatedItemDTO<BasicTeacherClassDTO>
                 (classDetails, totalRecordCount, request.Filter.CurrentPage + 1, request.Filter.PageSize);


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
