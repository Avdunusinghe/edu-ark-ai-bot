using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Enums;

namespace EduArk.Application.DTOs.UserDTOs
{
    public class UserDetailsFilterDTO : CorePaginationFilterDTO
    {
        public string? Name { get; set; }
        public int SelectedRole { get; set; }
        public UserActiveStatus UserActiveStatus { get; set; }

    }
}
