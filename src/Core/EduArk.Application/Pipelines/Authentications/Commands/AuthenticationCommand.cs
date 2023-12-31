using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.AuthenticationDTOs;
using EduArk.Domain.Entities.Master;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Master;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduArk.Application.Pipelines.Authentications.Commands
{
    public record AuthenticationCommand(AuthenticationDTO authenticationDetails) : IRequest<UserAuthenticationResponseDTO>;


    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, UserAuthenticationResponseDTO>
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly ITenantCompanyQueryRepository _tenantCompanyQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;

        public AuthenticationCommandHandler
            (IMediator mediator, IConfiguration configuration, ITenantCompanyQueryRepository _tenantCompanyQueryRepository, IUserQueryRepository userQueryRepository)
        {
            _mediator = mediator;
            _configuration = configuration;
            this._tenantCompanyQueryRepository = _tenantCompanyQueryRepository;
            _userQueryRepository = userQueryRepository;
        }
        public async Task<UserAuthenticationResponseDTO> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = (await _tenantCompanyQueryRepository
                            .Query(x => x.Domain.ToUpper().Trim() == request.authenticationDetails.Domain.ToUpper().Trim()))
                            .FirstOrDefault();

                if (tenant == null)
                {
                    return UserAuthenticationResponseDTO.NotSuccess
                        (ApplicationResponseConstant.SCHOOL_DOMAIN_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE);
                }

                var user = (await _userQueryRepository
                    .Query(x => x.UserName.Trim().ToUpper() == request.authenticationDetails.UserName.Trim().ToUpper() &&
                    x.IsActive == true))
                    .FirstOrDefault();

                if (user == null)
                {
                    return UserAuthenticationResponseDTO.NotSuccess
                        (ApplicationResponseConstant.USERNAME_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE);
                }

                if (!BCrypt.Net.BCrypt.Verify(request.authenticationDetails.Password, user.PasswordHash))
                {
                    return UserAuthenticationResponseDTO.NotSuccess
                        (ApplicationResponseConstant.PASSWORD_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE);
                }

                var userAthunticationResponse = await ConfigureJwtToken(user, tenant, cancellationToken);

                return userAthunticationResponse;
            }
            catch (Exception ex)
            {
                return UserAuthenticationResponseDTO.NotSuccess
                    (ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE);
            }
        }

        private async Task<UserAuthenticationResponseDTO> ConfigureJwtToken(User user, TenantCompany tenantCompany, CancellationToken cancellationToken)
        {
            var key = _configuration["Tokens:Key"];
            var issuer = _configuration["Tokens:Issuer"];

            string role = user.UserRoles.FirstOrDefault()!.Role.Name;

            var now = DateTime.UtcNow;
            DateTime nowDate = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tenantCompany.SecretKey.ToString()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Aud,"webapp"),
                new Claim(ClaimTypes.Role,role),
                new Claim("SecretKey",tenantCompany.SecretKey.ToString()),

            };

            var token = new JwtSecurityToken
            (
               issuer: issuer,
               claims: claims,
               expires: nowDate.AddHours(3),
               signingCredentials: credentials
            );

            token.Header.Add("kid", tenantCompany.APIKey.ToString());

            var tokenString = new JwtSecurityTokenHandler()
                            .WriteToken(token);

            var roles = user.UserRoles
                          .Select(t => t.Role)
                          .Select(t => t.Name)
                          .ToList();

            return UserAuthenticationResponseDTO.Success
               (
                tokenString,
                tenantCompany.Domain,
                $"{user.FirstName} {user.LastName}",
                user.Id,
                roles,
                user.ProfileImageUrl == null ? null : user.ProfileImageUrl
                );
        }
    }
}
