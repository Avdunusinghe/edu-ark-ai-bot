﻿using EduArk.Domain.Repositories.Command.Base;
using EduArk.Domain.Repositories.Query.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Domain.Repositories.Query.Tenant
{
    public interface IAssessmentQueryRepository : IQueryRepository<Assessment>
    {
    }
}
