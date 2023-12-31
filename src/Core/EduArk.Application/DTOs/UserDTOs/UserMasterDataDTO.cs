using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.UserDTOs
{
    public class UserMasterDataDTO
    {
        public UserMasterDataDTO()
        {
            Roles = new List<DropDownDTO>();
            UserActiveStatus = new List<DropDownDTO>();
        }
        public List<DropDownDTO> Roles { get; set; }
        public List<DropDownDTO> UserActiveStatus { get; set; }
    }
}
