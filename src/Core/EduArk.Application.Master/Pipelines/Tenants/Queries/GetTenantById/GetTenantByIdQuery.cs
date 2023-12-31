using EduArk.Application.Master.DTOs.TenantsDTO;
using EduArk.Domain.Repositories.Query.Master;
using MediatR;

namespace EduArk.Application.Master.Pipelines.Tenants.Queries.GetTenantById
{
    public record GetTenantByIdQuery(int id) : IRequest<TenantDetailsDTO>;

    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantDetailsDTO>
    {
        private readonly ITenantCompanyQueryRepository _tenantCompanyQueryRepository;
        public GetTenantByIdQueryHandler(ITenantCompanyQueryRepository tenantCompanyQueryRepository)
        {
            this._tenantCompanyQueryRepository = tenantCompanyQueryRepository;
        }
        public async Task<TenantDetailsDTO> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = await _tenantCompanyQueryRepository
                    .GetById(request.id, cancellationToken);  
                
                if (tenant == null)
                {
                    return new TenantDetailsDTO();
                }

                return tenant.ToDto();

            }
            catch (Exception ex)
            {

                return new TenantDetailsDTO();
            }
        }
    }


}
