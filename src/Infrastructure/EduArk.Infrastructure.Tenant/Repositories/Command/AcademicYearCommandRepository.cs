﻿using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Command.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Command
{
    public class AcademicYearCommandRepository : CommandRepository<AcademicYear>, IAcademicYearCommandRepository
    {
        public AcademicYearCommandRepository(TenantDbContext context) : base(context)
        {

        }
    }
}