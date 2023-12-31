using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Classes.Queries.GetClassesByFilter
{
    public record GetClassesByFilterQuery(ClassFilterDTO classFilter): IRequest<PaginatedItemDTO<ClassDetailDTO>>
    {
    }

    public class GetClassesByFilterQueryHandler : IRequestHandler<GetClassesByFilterQuery, PaginatedItemDTO<ClassDetailDTO>>
    {
        private readonly IClassQueryRepository _classQueryRepository;

        public GetClassesByFilterQueryHandler(IClassQueryRepository classQueryRepository)
        {
            this._classQueryRepository = classQueryRepository;
        }
        public async Task<PaginatedItemDTO<ClassDetailDTO>> Handle(GetClassesByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;
                var listOfActiveClass = await _classQueryRepository.Query(x => x.IsActive);

                if(!string.IsNullOrEmpty(request.classFilter.Name))
                {
                    listOfActiveClass = listOfActiveClass
                                        .Where(x => x.Name.Contains(request.classFilter.Name))
                                        .OrderBy(x => x.Name);
                }

                if(request.classFilter.AcademiclLevelId > 0)
                {
                    listOfActiveClass = listOfActiveClass
                                        .Where(x=>x.AcademicLevelId == request.classFilter.AcademiclLevelId)
                                        .OrderBy(x => x.Name);
                }

                if (request.classFilter.AcademicYearId > 0)
                {
                    listOfActiveClass = listOfActiveClass
                                        .Where(x => x.AcademicYearId == request.classFilter.AcademicYearId)
                                        .OrderBy(x => x.Name);
                }

                totalRecordCount = listOfActiveClass.Count();

                var listOfClasses = listOfActiveClass
                                    .OrderBy(x => x.Name)
                                    .Skip(request.classFilter.CurrentPage * request.classFilter.PageSize)
                                    .Take(request.classFilter.PageSize)
                                    .ToList();

                var availableListOfClasses = listOfClasses
                                            .Select(x=> x.ToClassDetailDto())
                                            .ToList();

                return new PaginatedItemDTO<ClassDetailDTO>
                          (
                              availableListOfClasses,
                              totalRecordCount,
                              request.classFilter.CurrentPage + 1,
                              request.classFilter.PageSize
                           );
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
