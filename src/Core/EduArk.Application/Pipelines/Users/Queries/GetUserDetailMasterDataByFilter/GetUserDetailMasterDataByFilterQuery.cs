using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Users.Queries.GetUserDetailMasterDataByFilter
{
    public record GetUserDetailMasterDataByFilterQuery(UserDetailsMasterDataFilterDTO filter) : IRequest<List<DropDownDTO>>
    {

    }

    public class GetUserDetailMasterDataByFilterQueryHander : IRequestHandler<GetUserDetailMasterDataByFilterQuery, List<DropDownDTO>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetUserDetailMasterDataByFilterQueryHander(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }
        public async Task<List<DropDownDTO>> Handle(GetUserDetailMasterDataByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var listOfUsers = await _userQueryRepository.Query(x => x.IsActive == true);

                if (!string.IsNullOrEmpty(request.filter.Name))
                {
                    listOfUsers = listOfUsers.Where(x => x.FirstName.Contains(request.filter.Name));
                }

                if(request.filter.RoleId > 0)
                {
                    listOfUsers = listOfUsers.Where(x => x.UserRoles.Any(x => x.RoleId == request.filter.RoleId));
                }

                var listOfAvailableUsers = listOfUsers.OrderBy(x => x.FirstName)
                                           .Take(10)
                                           .Select(x => new DropDownDTO()
                                            {
                                                Id = x.Id,
                                                Name = $"{x.FirstName} {x.LastName}",
                                            }).ToList();

                return listOfAvailableUsers;
            }
            catch (Exception ex)
            {

                return new List<DropDownDTO>();
            }
        }
    }
}
