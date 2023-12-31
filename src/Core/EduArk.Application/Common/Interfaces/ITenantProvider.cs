using EduArk.Application.DTOs.TenantDTOs;

namespace EduArk.Application.Common.Interfaces
{
    public interface ITenantProvider
    {
        Task<TenantDTO> GetTenant();
    }
}
