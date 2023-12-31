using EduArk.Application.Master.DTOs.CommonDTOs;
using EduArk.Application.Master.DTOs.TenantsDTO;
using EduArk.Domain.Entities.Master;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Query.Master;
using MediatR;

namespace EduArk.Application.Master.Pipelines.Tenants.Queries.GetTenantsByFilter
{
    public record GetTenantsByFilterQuery(TenantFilterDTO filter) : IRequest<PaginatedItemDTO<TenantDetailsDTO>>
    {
    }

    public class GetTenantsByFilterQueryHandler : IRequestHandler<GetTenantsByFilterQuery, PaginatedItemDTO<TenantDetailsDTO>>
    {
        private readonly ITenantCompanyQueryRepository _tenantCompanyQueryRepository;
        public GetTenantsByFilterQueryHandler(ITenantCompanyQueryRepository tenantCompanyQueryRepository)
        {
            this._tenantCompanyQueryRepository = tenantCompanyQueryRepository;
        }
        public async Task<PaginatedItemDTO<TenantDetailsDTO>> Handle(GetTenantsByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;

                var listOfTenants = await FilterTenants(request.filter, cancellationToken);

                totalRecordCount = listOfTenants.Count();

                var listOfAvailableTenants = listOfTenants.OrderByDescending(x => x.CreatedDate)
                                          .Skip(request.filter.CurrentPage * request.filter.PageSize)
                                          .Take(request.filter.PageSize)
                                          .Select(user => user.ToTenantDetailsDto())
                                          .ToList();

                return new PaginatedItemDTO<TenantDetailsDTO>
                            (   listOfAvailableTenants, 
                                totalRecordCount, 
                                request.filter.CurrentPage + 1, 
                                request.filter.PageSize
                            );


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<IEnumerable<TenantCompany>> FilterTenants(TenantFilterDTO filter, CancellationToken cancellationToken)
        {
            var listOfTenants = await _tenantCompanyQueryRepository
                        .GetAllListOfTenantCompaniesAsync(cancellationToken);

            if(!string.IsNullOrEmpty(filter.Name))
            {
                listOfTenants = listOfTenants.Where(x=>x.Name.Contains(filter.Name));
            }

            switch (filter.TenantTypeStatus)
            {
                case TenantTypeStatus.Public:

                    listOfTenants = listOfTenants
                                    .Where(x => x.IsGovernmentSchool == true);
                    break;

                case TenantTypeStatus.Private:
                    listOfTenants = listOfTenants
                                    .Where(x => x.IsGovernmentSchool == false);
                    break;

                default:
                    break;
            }


            return listOfTenants;
        }
    }
}
