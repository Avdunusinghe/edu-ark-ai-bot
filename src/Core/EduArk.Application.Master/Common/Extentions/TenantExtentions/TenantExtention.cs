using EduArk.Application.Master.DTOs.TenantsDTO;
using EduArk.Domain.Entities.Master;
using MediatR;

namespace System
{
    public static class TenantExtention
    {
        public static TenantCompany ToEntity(this TenantDetailsDTO dto, TenantCompany? tenant = null)
        {
            if (tenant == null)
            {
                tenant = new TenantCompany();
            }

            tenant.Name = dto.Name;
            tenant.Domain = dto.Domain;
            tenant.SMTPServer = dto.SMTPServer;
            tenant.SMTPUsername = dto.SMTPUsername;
            tenant.SMTPPassword = dto.SMTPPassword;
            tenant.SMTPFrom = dto.SMTPFrom;
            tenant.SMTPPort = dto.SMTPPort;
            tenant.IsSMTPUseSSL = dto.IsSMTPUseSSL;
            tenant.IsGovernmentSchool = dto.IsGovernmentSchool;
            tenant.SpecialNotes = dto.SpecialNotes;
          


            if (tenant.Id == 0)
            {
                tenant.CreatedDate = DateTime.UtcNow;
                tenant.IsActive = true;
                tenant.TenantId = Guid.NewGuid();
                tenant.APIKey = Guid.NewGuid();
                tenant.SecretKey = Guid.NewGuid();
            }
            

            return tenant;

        }

        public static TenantDetailsDTO ToTenantDetailsDto(this TenantCompany entity, TenantDetailsDTO? dto = null)
        {
            if (dto == null)
            {
                dto = new TenantDetailsDTO();
            }

            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Domain = entity.Domain;
            dto.SMTPServer = entity.SMTPServer;
            dto.ConnectionString = entity.ConnectionString;
            dto.SMTPUsername = entity.SMTPUsername;
            dto.SMTPFrom = entity.SMTPFrom;
            dto.SMTPPort = entity.SMTPPort;
            dto.IsSMTPUseSSL = entity.IsSMTPUseSSL;
            dto.SpecialNotes = entity.SpecialNotes ?? string.Empty;
            dto.IsGovernmentSchool = entity.IsGovernmentSchool;

            return dto;
        }

        public static TenantDetailsDTO ToDto(this TenantCompany entity, TenantDetailsDTO? dto = null)
        {
            if (dto == null)
            {
                dto = new TenantDetailsDTO();
            }

            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Domain = entity.Domain;
            dto.SMTPServer = entity.SMTPServer;
            dto.ConnectionString = entity.ConnectionString;
            dto.SMTPUsername = entity.SMTPUsername;
            dto.SMTPFrom = entity.SMTPFrom;
            dto.SMTPPort = entity.SMTPPort;
            dto.IsSMTPUseSSL = entity.IsSMTPUseSSL;
            dto.SpecialNotes = entity.SpecialNotes ?? string.Empty;
            dto.IsGovernmentSchool = entity.IsGovernmentSchool;

            return dto;
        }
    }
}
