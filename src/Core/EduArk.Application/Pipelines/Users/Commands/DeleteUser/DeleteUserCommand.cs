using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int id) : IRequest<ResultDTO>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultDTO>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;

        /// <summary>
        /// Constructor for DeleteUserCommandHandler
        /// </summary>
        /// <param name="userQueryRepository">User query repository</param>
        /// <param name="userCommandRepository">User command repository</param>
        /// 
        public DeleteUserCommandHandler(IUserQueryRepository userQueryRepository, IUserCommandRepository userCommandRepository)
        {
            this._userQueryRepository = userQueryRepository;
            this._userCommandRepository = userCommandRepository;
        }

        /// <summary>
        /// Executes the command to delete a user by setting their IsActive property to false.
        /// </summary>
        /// <param name="request">The command to delete a user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the command.</returns>
        public async Task<ResultDTO> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userQueryRepository.GetById(request.id, cancellationToken);

                if(user == null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.USER_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE,
                    });
                }
                else
                {
                    user.IsActive = false;

                    await _userCommandRepository.UpdateAsync(user, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.USER_DELETE_SUCCESS_RESPONSE_MEESSAGE);
                }

            }catch (Exception ex)
            {
                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE,
                });
            }
        }
    }
}
