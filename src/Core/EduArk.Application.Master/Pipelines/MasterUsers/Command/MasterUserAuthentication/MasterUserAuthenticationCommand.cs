using EduArk.Application.Master.Common.Constants;
using EduArk.Application.Master.DTOs.AuthenticationDTOs;
using EduArk.Domain.Entities.Master;
using EduArk.Domain.Repositories.Query.Master;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduArk.Application.Master.Pipelines.MasterUsers.Command.MasterUserAuthentication
{
    public record MasterUserAuthenticationCommand(AuthenticationDTO dto) : IRequest<AuthenticationResponseDTO>;


    public record MasterUserAuthenticationCommandHandler : IRequestHandler<MasterUserAuthenticationCommand, AuthenticationResponseDTO>
    {
        private readonly IMasterUserQueryRepository _masterUserQueryRepository;
        private readonly IConfiguration _configuration;
        public MasterUserAuthenticationCommandHandler(IMasterUserQueryRepository masterUserQueryRepository, IConfiguration configuration)
        {
            this._masterUserQueryRepository = masterUserQueryRepository;
            this._configuration = configuration;
        }
        public async Task<AuthenticationResponseDTO> Handle(MasterUserAuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var user = (await _masterUserQueryRepository.Query(x=>x.Email == request.dto.UserName))
                           .FirstOrDefault();

                if (user == null)
                {
                    return AuthenticationResponseDTO
                        .NotSuccess
                        (
                            ApplicationResponseConstant.USERNAME_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE
                        );
                }

                if(!BCrypt.Net.BCrypt.Verify(request.dto.Password, user.PasswordHash))
                {
                    return AuthenticationResponseDTO
                        .NotSuccess
                        (
                        ApplicationResponseConstant.PASSWORD_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE
                        );
                }

                var authenticationResponse = await ConfigureJwtToken(user, cancellationToken);

                 return authenticationResponse;

               
            }
            catch (Exception ex)
            {

                return AuthenticationResponseDTO.NotSuccess(string.Empty);
            }
        }

        private async Task<AuthenticationResponseDTO> ConfigureJwtToken(MasterUser user, CancellationToken cancellationToken)
        {
            var key = _configuration["Tokens:Key"];
            var issuer = _configuration["Tokens:Issuer"];

    

            var now = DateTime.UtcNow;
            DateTime nowDate = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("firstName",string.IsNullOrEmpty(user.FirstName)? "": user.FirstName),
                new Claim("lastName", string.IsNullOrEmpty(user.LastName) ? "" : user.LastName),
                new Claim(JwtRegisteredClaimNames.Iat,now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Aud,"webapp"),
              

            };

            var token = new JwtSecurityToken
            (
               issuer: issuer,
               claims: claims,
               expires: nowDate.AddHours(3),
               signingCredentials: credentials
            );

          
            var tokenString = new JwtSecurityTokenHandler()
                            .WriteToken(token);

          

            return AuthenticationResponseDTO.Success
               (
                tokenString,
                $"{user.FirstName} {user.LastName}",
                user.Id,
                new List<string>()
                );
        }
    }
}
