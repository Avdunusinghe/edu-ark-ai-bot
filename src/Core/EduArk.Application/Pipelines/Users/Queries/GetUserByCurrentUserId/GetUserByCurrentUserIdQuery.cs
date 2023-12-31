using EduArk.Application.Common.Extensions;
using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Users.Queries.GetUserByCurrentUserId
{
    public record GetUserByCurrentUserIdQuery() : IRequest<UserDetailsDTO>
    {
    }

    public class GetUserByCurrentUserIdQueryHandler : IRequestHandler<GetUserByCurrentUserIdQuery, UserDetailsDTO>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserQueryRepository _userQueryRepository;

        public GetUserByCurrentUserIdQueryHandler(ICurrentUserService currentUserService, IUserQueryRepository userQueryRepository)
        {
            this._currentUserService = currentUserService;
            this._userQueryRepository = userQueryRepository;
        }
        public async Task<UserDetailsDTO> Handle(GetUserByCurrentUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = await _userQueryRepository
                                  .GetById
                                    (
                                        _currentUserService.UserId!.Value,
                                        cancellationToken
                                    );

                if ( loggedInUser != null )
                {
                    return loggedInUser.ToDto();
                }
                else
                {
                    return new UserDetailsDTO();
                }
                
            }
            catch (Exception ex)
            {

                return new UserDetailsDTO();
            }
        }
    }
}
