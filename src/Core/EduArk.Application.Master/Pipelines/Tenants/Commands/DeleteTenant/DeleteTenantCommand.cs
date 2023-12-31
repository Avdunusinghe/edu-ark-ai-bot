using EduArk.Application.Master.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Master;
using EduArk.Domain.Repositories.Query.Master;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Application.Master.Pipelines.Tenants.Commands.DeleteTenant
{
    public record DeleteTenantCommand(int id) : IRequest<ResultDTO>
    {
    }



    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, ResultDTO>
    {
        private readonly ITenantCompanyQueryRepository _tenantCompanyQueryRepository;
        private readonly ITenantCompanyCommandRepository _tenantCompanyCommandRepository;
        public DeleteTenantCommandHandler
            (ITenantCompanyCommandRepository tenantCompanyCommandRepository, ITenantCompanyQueryRepository tenantCompanyQueryRepository)
        {
            this._tenantCompanyCommandRepository = tenantCompanyCommandRepository;
            this._tenantCompanyQueryRepository = tenantCompanyQueryRepository;
        }

        public async Task<ResultDTO> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = await _tenantCompanyQueryRepository.GetById(request.id, cancellationToken);

                if (tenant == null)
                {

                    return ResultDTO.Failure(new List<string>()
                    {
                        "Tenant not found"
                    });

                }
                else
                {
                    tenant.IsActive = false;

                    await _tenantCompanyCommandRepository.UpdateAsync(tenant, cancellationToken);

                    return ResultDTO.Success("Tenant Delete has been suceefully");

                }


            }
            catch (Exception)
            {

                return ResultDTO.Failure(new List<string>()
                {
                        "Error has been occured please try again"
                });
            }
        }
    }
}
