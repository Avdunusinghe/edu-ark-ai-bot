using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.AuthenticationDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Infrastructure.Master.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace EduArk.Infrastructure.Master.Services
{
    public class SqlServerTenantProvider : ITenantProvider
    {
       
        private readonly MasterDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private List<TenantDTO> tenants;
        public SqlServerTenantProvider(IHttpContextAccessor accessor, MasterDbContext context)
        {
            this._accessor = accessor;
            this._context = context;
            LoadTenants();

        }

        public async Task<TenantDTO> GetTenant()
        {
            try
            {
                if (_accessor.HttpContext == null)
                    return null;

               

                var identity = _accessor.HttpContext.User?.Identity as ClaimsIdentity;
                if (identity.Claims.Count() > 0)
                {
                    var secretKey = identity.FindFirst("SecretKey").Value;

                    var machingTenant = tenants.FirstOrDefault(x => x.SecretKey.ToUpper() == secretKey.ToUpper());

                    return machingTenant;
                }
                else
                {
                    
                    var loginRequest = _accessor.HttpContext.Request.Body;
                    using (var reader = new StreamReader(loginRequest, Encoding.UTF8))
                    {
                        var value = await reader.ReadToEndAsync();

                        var loginVm = JsonConvert.DeserializeObject<AuthenticationDTO>(value);

                        var matchingTenantSchool = tenants.FirstOrDefault(x => x.Domain.ToUpper() == loginVm.Domain.ToUpper());

                        _accessor.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(value));

                        return matchingTenantSchool;
                    }

                }

                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        private void LoadTenants()
        {
            tenants = _context.Tenants.Where(x => x.IsActive == true).Select(x => new TenantDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Domain = x.Domain,
                Logo = x.Logo,
                ConnectionString = x.ConnectionString,
                SMTPServer = x.SMTPServer,
                SMTPUsername = x.SMTPUsername,
                SMTPPassword = x.SMTPPassword,
                SMTPFrom = x.SMTPFrom,
                TenantId = x.TenantId.ToString(),
                APIKey = x.APIKey.ToString(),
                SecretKey = x.SecretKey.ToString(),
                IsActive = x.IsActive
            }).ToList();
        }



     
    }
}
