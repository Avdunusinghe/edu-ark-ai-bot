using EduArk.Application.Master.Common.Constants;
using EduArk.Application.Master.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Master;
using MediatR;

namespace EduArk.Application.Master.Pipelines.Tenants.Queries.ValidateTenantDomain
{
    public record ValidateTenantDomainQuery(string domain) : IRequest<ResultDTO>
    {
    }

    public class ValidateTenantDomainQueryHandler : IRequestHandler<ValidateTenantDomainQuery, ResultDTO>
    {
        private readonly ITenantCompanyQueryRepository _tenantCompanyQueryRepository;

        public ValidateTenantDomainQueryHandler(ITenantCompanyQueryRepository tenantCompanyQueryRepository)
        {
            this._tenantCompanyQueryRepository = tenantCompanyQueryRepository;
        }
        public async Task<ResultDTO> Handle(ValidateTenantDomainQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validatedDomainData = (await _tenantCompanyQueryRepository
                                      .Query(x => x.Domain == request.domain))
                                      .FirstOrDefault();

                if (validatedDomainData == null)
                {
                    return ResultDTO.Success(string.Empty);
                }
                else
                {

                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.TENANT_DOMAIN_ALL_READY_EXSIST_RESPONSE_MESSAGE
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            

           
        }
    }
}
