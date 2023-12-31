using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.ClassNameDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.ClassNames.Queries.GetClassNamesByFilter
{
    public record GetClassNamesByFilterQuery(ClassNameFilterDTO classNameFilter) 
        : IRequest<PaginatedItemDTO<ClassNameDTO>>
    { 
    }

    public class GetClassNamesByFilterQueryHandler 
        : IRequestHandler<GetClassNamesByFilterQuery, PaginatedItemDTO<ClassNameDTO>>
    {
        private readonly IClassNameQueryRepository _classNameQueryRepository;

        public GetClassNamesByFilterQueryHandler(IClassNameQueryRepository classNameQueryRepository)
        {
            this._classNameQueryRepository = classNameQueryRepository;
        }
        public async Task<PaginatedItemDTO<ClassNameDTO>> Handle(GetClassNamesByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;
                var classNames = await _classNameQueryRepository.Query(x => x.IsActive);

                if (!string.IsNullOrEmpty(request.classNameFilter.Name))
                {
                    classNames = classNames.Where(x => x.Name.Contains(request.classNameFilter.Name));
                }

                totalRecordCount = classNames.Count();

                var classNamesDataSet = classNames.OrderByDescending(x => x.CreatedDate)
                                          .Skip(request.classNameFilter.CurrentPage * request.classNameFilter.PageSize)
                                          .Take(request.classNameFilter.PageSize)
                                          .ToList();


                var availableClassNameDataSet = classNamesDataSet
                                                .Select(x => x.ToClassNameDto())
                                                .ToList();

                return new PaginatedItemDTO<ClassNameDTO>
                           (
                               availableClassNameDataSet, 
                               totalRecordCount, 
                               request.classNameFilter.CurrentPage + 1, 
                               request.classNameFilter.PageSize
                            );

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
