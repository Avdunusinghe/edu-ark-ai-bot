using System.Runtime.CompilerServices;

namespace EduArk.Application.DTOs.AuthenticationDTOs
{
    public class UserAuthenticationResponseDTO
    {
        internal UserAuthenticationResponseDTO
            (bool isLoginSuccess, string token, string tenantDomain, string displayName, int userId, string message, List<string> roles, string? profileImageUrl)
        {
            this.IsLoginSuccess = isLoginSuccess;
            this.Token = token;
            this.TenantDomain = tenantDomain;
            this.DisplayName = displayName;
            this.UserId = userId;
            this.Message = message;
            this.Roles = roles;
            this.ProfileImageUrl = profileImageUrl;
        }

        public bool IsLoginSuccess { get; set; }
        public string Token { get; set; }
        public string TenantDomain { get; set; }
        public string DisplayName { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public List<string> Roles { get; set; }
        public string? ProfileImageUrl { get; set; }

        public static UserAuthenticationResponseDTO NotSuccess(string errorMessage)
        {
            return new UserAuthenticationResponseDTO(false, string.Empty, string.Empty, string.Empty, 0, errorMessage, new List<string>(), string.Empty);
        }

        public static UserAuthenticationResponseDTO Success(string token, string tenantDomain, string displayName, int userId, List<string> roles, string profileImageUrl)
        {
            return new UserAuthenticationResponseDTO(true, token, tenantDomain, displayName, userId, string.Empty, roles, profileImageUrl);
        }
    }

}
