using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.AcademicLevels.Commands.DeleteAcademicLevel
{
    public record DeleteAcademicLevelCommand(int id) : IRequest<ResultDTO>;
  
    public class DeleteAcademicLevelCommandHandler : IRequestHandler<DeleteAcademicLevelCommand, ResultDTO>
    {
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly IAcademicLevelCommandRepository _academicLevelCommandRepository;

        /// <summary>
        /// Constructor for DeleteAcademicLevelCommandHandler class
        /// </summary>
        /// <param name="academicLevelQueryRepository">academicLevel Query Repository</param>
        /// <param name="academicLevelCommandRepository">academicLevel Command Repository</param>
        public DeleteAcademicLevelCommandHandler(IAcademicLevelQueryRepository academicLevelQueryRepository, IAcademicLevelCommandRepository academicLevelCommandRepository)
        {
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._academicLevelCommandRepository = academicLevelCommandRepository;

        }

        /// <summary>
        /// Handles the DeleteAcademicLevelCommand request
        /// </summary>
        /// <param name="request">DeleteAcademicLevelCommand request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>ResultDTO indicating the result of the operation</returns>
        public async Task<ResultDTO> Handle(DeleteAcademicLevelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the academic level entity by its Id
                var academicLevel = await _academicLevelQueryRepository.GetById(request.id, cancellationToken);

                if (academicLevel == null)
                {
                    // If the academic level does not exist, return a failure result
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.ACADEMIC_LEVEL_NOT_AVAILABLE_RESPONSE_MESSAGE
                    });
                }
                else
                {
                    // Set the IsActive property to false to mark the academic level as inactive
                    academicLevel.IsActive = false;

                    // Update the academic level entity
                    await _academicLevelCommandRepository.UpdateAsync(academicLevel, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.ACADEMIC_LEVEL_DELETE_SUCCESS_RESPONSE_MESSAGE);
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that occur during the operation and return a failure result
                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE,
                });
            }
        }
    }
}
