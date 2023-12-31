using EduArk.Application.Master.DTOs.CommonDTOs;
using EduArk.Domain.Enums;

namespace EduArk.Application.Master.DTOs.TenantsDTO
{
    public class TenantFilterDTO : CorePaginationFilterDTO
    {
        public string Name { get; set; }
        public TenantTypeStatus TenantTypeStatus { get; set; }


      
    }
}
