using EduArk.Application.Common.Constants;
using EduArk.Application.Common.Extensions;
using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using System.Security.Cryptography;

namespace EduArk.Application.Pipelines.Users.Commands.SaveUser
{
    public record SaveUserCommand(UserDetailsDTO UserDetails) : IRequest<ResultDTO>;

    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, ResultDTO>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Constructor for SaveUserCommandHandler
        /// </summary>
        /// <param name="userQueryRepository">User query repository</param>
        /// <param name="userCommandRepository">User command repository</param>
        /// <param name="currentUserService">Current user service</param>
        /// 
        public SaveUserCommandHandler
            (IUserQueryRepository userQueryRepository, 
            IUserCommandRepository userCommandRepository,
            ICurrentUserService currentUserService)
        {
            this._userQueryRepository = userQueryRepository;
            this._userCommandRepository = userCommandRepository;
            this._currentUserService = currentUserService;
        }

        /// <summary>
        /// Handler method for SaveUserCommand
        /// </summary>
        /// <param name="request">SaveUserCommand</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result DTO</returns>
        public async Task<ResultDTO> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
              
                var user = await _userQueryRepository
                                .GetById(request.UserDetails.Id, cancellationToken);

                if (user == null)
                {
                    user = request.UserDetails.ToEntity();

                    AddNewUserRoles(user, request.UserDetails.Roles);

                     await _userCommandRepository.AddAsync(user, cancellationToken);

                    return ResultDTO
                           .Success(
                                    ApplicationResponseConstant.USER_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE, 
                                    user.Id
                                    );
                }
                else
                {
                    user = request.UserDetails.ToEntity(user);

                    var exsistingUserRoles = user.UserRoles.ToList();
                    var selectedUserRoles = request.UserDetails.Roles.ToList();

                    var newUserRoles = (from role in selectedUserRoles 
                                        where exsistingUserRoles
                                        .All(x => x.RoleId != role) select role)
                                        .ToList();

                    var deletedUserRoles = (from role in exsistingUserRoles 
                                            where selectedUserRoles
                                            .All(x => x != role.RoleId) select role)
                                            .ToList();

                    if(newUserRoles.Count > 0)
                    {
                        AddNewUserRoles(user, newUserRoles);

                    }


                    foreach (var role in deletedUserRoles)
                    {
                        user.UserRoles.Remove(role);
                    }

                    await _userCommandRepository.UpdateAsync(user, cancellationToken);

                    return ResultDTO
                          .Success
                          (
                            ApplicationResponseConstant.USER_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE,
                            user.Id
                          );
                }
            }
            catch (Exception ex)
            {

                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE,
                });
            }
        }

        /// <summary>
        /// Adds new user roles to the user entity object
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="roles">List<int></param>
        private void AddNewUserRoles(User user, List<int> roles)
        {
            foreach (var roleId in roles)
            {
                user.UserRoles.Add(new UserRole()
                {
                    UserId = user.Id,
                    RoleId = roleId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    CreatedByUserId = _currentUserService.UserId!.Value,
                    UpdateDate = DateTime.UtcNow,
                    UpdatedByUserId = _currentUserService.UserId!.Value,
                });
            }

        }
    }
}
