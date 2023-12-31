namespace EduArk.Application.Master.DTOs.AuthenticationDTOs
{
    public class AuthenticationResponseDTO
    {
        internal AuthenticationResponseDTO
            (bool isLoginSuccess, string token, string displayName, int userId, string message, List<string> roles)
        {
            this.IsLoginSuccess = isLoginSuccess;
            this.Token = token;
            this.DisplayName = displayName;
            this.UserId = userId;
            this.Message = message;
            this.Roles = roles;
        }

        public bool IsLoginSuccess { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public List<string> Roles { get; set; }

        public static AuthenticationResponseDTO NotSuccess(string errorMessage)
        {
            return new AuthenticationResponseDTO(false, string.Empty,  string.Empty, 0, errorMessage, new List<string>());
        }

        public static AuthenticationResponseDTO Success(string token, string displayName, int userId, List<string> roles)
        {
            return new AuthenticationResponseDTO(true, token, displayName, userId, string.Empty, roles);
        }
    }
}
