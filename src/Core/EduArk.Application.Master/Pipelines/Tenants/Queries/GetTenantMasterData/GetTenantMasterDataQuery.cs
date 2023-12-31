using EduArk.Application.Master.Common.Helper;
using EduArk.Application.Master.DTOs.CommonDTOs;
using EduArk.Application.Master.DTOs.TenantsDTO;
using EduArk.Domain.Enums;
using MediatR;

namespace EduArk.Application.Master.Pipelines.Tenants.Queries.GetTenantMasterData
{
    public record GetTenantMasterDataQuery : IRequest<TenantMasterDataDTO>
    {
    }

    public class GetTenantMasterDataQueryHandler : IRequestHandler<GetTenantMasterDataQuery, TenantMasterDataDTO>
    {
        
        public async Task<TenantMasterDataDTO> Handle(GetTenantMasterDataQuery request, CancellationToken cancellationToken)
        {
            var response = new TenantMasterDataDTO();

            response.TenantTypeStatus = Enum.GetValues(typeof(TenantTypeStatus))
                    .Cast<TenantTypeStatus>()
                    .Select(tenantTypeStatus => new DropDownDTO
                     {
                         Id = (int)tenantTypeStatus,
                         Name = EnumHelper.GetEnumDescription(tenantTypeStatus)
                     })
                     .ToList();

            response.ServerDetails.Add(
                 new ServerDetailDropDownDTO
                 {
                     Id = 0,
                     Name = "East Us Azure Central Server",
                     ServerName = "eduark"
                 });

            response.ServerDetails.Add(
                 new ServerDetailDropDownDTO
                {
                    Id = 0,
                    Name = "EduArk Main Local Server",
                    ServerName = "ASHEN"
                });

            response.ServerDetails.Add(
                new ServerDetailDropDownDTO
                {
                    Id = 0,
                    Name = "EduArk Main Local Server - S",
                    ServerName = "DESKTOP-D32ORJ1\\SQLEXPRESS"
                });

            response.ServerDetails.Add(
                new ServerDetailDropDownDTO
                {
                    Id = 0,
                    Name = "EduArk Main Local Server - C",
                    ServerName = "DESKTOP-L5IG0M5\\SQLEXPRESS"
                });

            response.ServerDetails.Add(
                new ServerDetailDropDownDTO
                {
                    Id = 0,
                    Name = "EduArk Main Local Server - D",
                    ServerName = "DESKTOP-JTSNI0P\\SQLEXPRESS01"
                });

            return response;
        }
    }
}
