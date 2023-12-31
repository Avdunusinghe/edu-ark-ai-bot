using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Users.Queries.GetUserDetailsByFilter
{
    public record GetUserDetailsByFilterQuery(UserDetailsFilterDTO filter) 
        : IRequest<PaginatedItemDTO<UserDetailsDTO>>;


    public class GetUserDetailsByFilterQueryQuery 
                            : IRequestHandler<GetUserDetailsByFilterQuery, PaginatedItemDTO<UserDetailsDTO>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        /// <summary>
        /// Constructor for GetUserDetailsByFilterQueryQuery class
        /// </summary>
        /// <param name="userQueryRepository">User query repository</param>
        public GetUserDetailsByFilterQueryQuery(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }

        /// <summary>
        /// Retrieves user details based on a filter criteria
        /// </summary>
        /// <param name="request">Request object with filter criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paginated item DTO containing user details that match the filter criteria</returns>
        public async Task<PaginatedItemDTO<UserDetailsDTO>> Handle(GetUserDetailsByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecordCount = 0;

                var listOfUsers = await FilterUserDetails(request.filter, cancellationToken);

                totalRecordCount = listOfUsers.Count();

                var listOfAvailableUsers = listOfUsers.OrderByDescending(x=>x.CreatedDate)
                                           .Skip(request.filter.CurrentPage * request.filter.PageSize)
                                           .ToList();


                var availableDataSet = listOfAvailableUsers
                                       .Select(user => new UserDetailsDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    PhoneNumber = user.PhoneNumber,
                    CreatedDate = user.CreatedDate.ToString("MM/dd/yyyy"),
                    CreatedUser = user.CreatedByUserId.HasValue ? user.CreatedByUser.FirstName : string.Empty,
                    UpdatedDate = user.UpdateDate?.ToString("MM/dd/yyyy"),
                    Roles = user.UserRoles.Select(x=>x.RoleId).ToList(),
                    

                }).ToList();

                return new PaginatedItemDTO<UserDetailsDTO>
                    (availableDataSet, totalRecordCount, request.filter.CurrentPage + 1, request.filter.PageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Filters the list of users based on the filter criteria
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of users that match the filter criteria</returns>

        private async Task<IQueryable<User>> FilterUserDetails(UserDetailsFilterDTO filter, CancellationToken cancellationToken)
        {
            var listOfUsers = await _userQueryRepository.GetAllListOfUsersAsync(cancellationToken);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                listOfUsers = listOfUsers.Where(x=>x.FirstName.Contains(filter.Name));
            }

            switch(filter.UserActiveStatus)
            {
                case UserActiveStatus.Active:

                    listOfUsers = listOfUsers
                                  .Where(x=>x.IsActive == true); 
                    break;

               case UserActiveStatus.Inactive:
           
                    listOfUsers = listOfUsers
                                  .Where(x => x.IsActive == false);
                    break;

                default:
                    break;

            }

            if(filter.SelectedRole > 0)
            {
                listOfUsers = listOfUsers
                             .Where(x=>x.UserRoles
                             .All(x=>x.RoleId == filter.SelectedRole));
            }

            return listOfUsers;


        }
    }
}
