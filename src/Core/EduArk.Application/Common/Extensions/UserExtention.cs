using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Entities.Master;
using EduArk.Domain.Entities.Tenant;
using System.Data;

namespace EduArk.Application.Common.Extensions
{
    public static class UserExtention
    {
        public static User ToEntity(this UserDetailsDTO userDetailsDTO, User? user = null)
        {
            if(user == null) 
            { 
                user = new User();
            }

            user.FirstName = userDetailsDTO.FirstName;
            user.LastName = userDetailsDTO.LastName;
            user.Email = userDetailsDTO.Email;
            user.IsActive = true;
            if(user.Id == 0)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("changeme");
            }

            user.UserName = userDetailsDTO.UserName;
            user.PhoneNumber = userDetailsDTO.PhoneNumber;

            return user;

        }

        public static UserDetailsDTO ToDto(this User entity, UserDetailsDTO? dto = null)
        {
            if (dto == null)
            {
                dto = new UserDetailsDTO();
            }
            dto.Id = entity.Id;
            dto.FirstName = entity.FirstName;
            dto.LastName = entity.LastName;
            dto.Email = entity.Email;
            dto.UserName = entity.UserName;
            dto.PhoneNumber = entity.PhoneNumber;
            dto.RoleName = entity.UserRoles.FirstOrDefault()!.Role.Name;
            dto.CreatedDate = entity.CreatedDate.ToString("MM/dd/yyyy");
            dto.CreatedUser = entity.CreatedByUserId.HasValue ? entity.CreatedByUser.FirstName : string.Empty;
            dto.UpdatedDate = entity.UpdateDate?.ToString("MM/dd/yyyy");
            dto.Roles = entity.UserRoles.Select(x => x.RoleId).ToList();
            dto.ProfileImageUrl = entity.ProfileImageUrl ?? string.Empty;

            return dto;
        }
    }
}
