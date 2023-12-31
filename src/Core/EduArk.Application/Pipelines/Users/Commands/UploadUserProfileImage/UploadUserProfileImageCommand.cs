using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using System.ComponentModel;

namespace EduArk.Application.Pipelines.Users.Commands.UploadUserProfileImage
{
    public record UploadUserProfileImageCommand(BlobContainerDTO BlobContainer)
                        : IRequest<ResultDTO>;

    public class UploadUserProfileImageCommandHandler : IRequestHandler<UploadUserProfileImageCommand, ResultDTO>
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;

        public UploadUserProfileImageCommandHandler
            (
                IAzureBlobStorageService azureBlobStorageService,
                IUserQueryRepository userQueryRepository,
                IUserCommandRepository userCommandRepository
            )
        {
            this._azureBlobStorageService = azureBlobStorageService;
            this._userQueryRepository = userQueryRepository;
            this._userCommandRepository = userCommandRepository;
        }
        public async Task<ResultDTO> Handle(UploadUserProfileImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _azureBlobStorageService
                                .UploadAsync(request.BlobContainer);

                var user = await _userQueryRepository
                                .GetById(request.BlobContainer.Id, cancellationToken);

                user.ProfileImageUrl = response.Url;

                await _userCommandRepository.UpdateAsync(user, cancellationToken);

                return ResultDTO.Success(string.Empty);
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }
    }




}
