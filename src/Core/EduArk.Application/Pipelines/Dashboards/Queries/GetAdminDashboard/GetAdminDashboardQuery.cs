using EduArk.Application.DTOs.DashboardDTOs;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Dashboards.Queries.GetAdminDashboard
{
    public class GetAdminDashboardQuery : IRequest<AdminDashboardDTO>
    {
    }

    public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDTO>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IClassQueryRepository _classQueryRepository;
        public GetAdminDashboardQueryHandler
        (
            IUserQueryRepository userQueryRepository,
            IAcademicYearQueryRepository academicYearQueryRepository,
            IClassQueryRepository classQueryRepository
        )
        {
            this._userQueryRepository = userQueryRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._classQueryRepository = classQueryRepository;
        }
        public async Task<AdminDashboardDTO> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var adminDashboardData = new AdminDashboardDTO();

                adminDashboardData.CurrentAcademicYear = (await _academicYearQueryRepository.Query(x => x.IsCurrentYear == true))
                                                           .FirstOrDefault().Id;

                adminDashboardData.AcademicUsersCount = (await _userQueryRepository
                                                        .Query(x=>x.IsActive == true && x.UserRoles
                                                        .Any(r=>r.RoleId != (int)RoleType.Student)))
                                                        .ToList()
                                                        .Count();   

                adminDashboardData.StudentCount = (await _userQueryRepository
                                                 .Query(x => x.IsActive == true && x.UserRoles
                                                 .Any(r => r.RoleId == (int)RoleType.Student)))
                                                 .ToList()
                                                 .Count();

                

                adminDashboardData.ClassCount = (await _classQueryRepository
                                                .Query(x=>x.IsActive == true && x.AcademicYearId == adminDashboardData.CurrentAcademicYear))
                                                .ToList()
                                                .Count();

                return adminDashboardData;


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
