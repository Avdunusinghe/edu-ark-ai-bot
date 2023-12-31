using EduArk.Application.Master.DTOs.CommonDTOs;

namespace EduArk.Application.Master.DTOs.TenantsDTO
{
    public class TenantMasterDataDTO
    {
        public TenantMasterDataDTO()
        {
            TenantTypeStatus = new List<DropDownDTO>();
            ServerDetails = new List<ServerDetailDropDownDTO>();
        }
        public List<DropDownDTO> TenantTypeStatus { get; set; }
        public List<ServerDetailDropDownDTO> ServerDetails { get; set; }
    }
}
