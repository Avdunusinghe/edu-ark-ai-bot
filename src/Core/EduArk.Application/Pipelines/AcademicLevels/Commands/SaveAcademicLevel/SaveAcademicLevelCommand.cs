using EduArk.Application.Common.Constants;
using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.AcademicLevelDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.AcademicLevels.Commands.SaveAcademicLevel
{
    public record SaveAcademicLevelCommand(AcademicLevelDTO academicLevelDetails) : IRequest<ResultDTO>;

    public class SaveAcademicLevelCommandHandler : IRequestHandler<SaveAcademicLevelCommand, ResultDTO>
    {
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly IAcademicLevelCommandRepository _academicLevelCommandRepository;

        /// <summary>
        /// Constructor for SaveAcademicLevelCommandHandler class
        /// </summary>
        /// <param name="academicLevelQueryRepository">academicLevel Query Repository</param>
        /// <param name="academicLevelCommandRepository">academicLevel Command Repository</param>
        public SaveAcademicLevelCommandHandler(IAcademicLevelQueryRepository academicLevelQueryRepository, IAcademicLevelCommandRepository academicLevelCommandRepository)
        {
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._academicLevelCommandRepository = academicLevelCommandRepository;

        }

        /// <summary>
        /// Handles the SaveAcademicLevelCommand request
        /// </summary>
        /// <param name="request">SaveAcademicLevelCommand request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>ResultDTO indicating the result of the operation</returns>
        public async Task<ResultDTO> Handle(SaveAcademicLevelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the academic level entity by its Id
                var academicLevel = await _academicLevelQueryRepository.GetById(request.academicLevelDetails.Id, cancellationToken);

                if (academicLevel == null)
                {
                    // If the academic level does not exist, create a new entity from the provided details
                    academicLevel = request.academicLevelDetails.ToEntity();

                    // Add the new academic level entity
                    var newAcademicLevel = await _academicLevelCommandRepository.AddAsync(academicLevel, cancellationToken);

                    return ResultDTO.Success(
                        ApplicationResponseConstant.ACADEMIC_LEVEL_SAVE_SUCCESS_RESPONSE_MESSAGE, newAcademicLevel.Id);


                }
                else
                {
                    // If the academic level exists, update the existing entity with the provided details
                    academicLevel = request.academicLevelDetails.ToEntity(academicLevel);

                    await _academicLevelCommandRepository.UpdateAsync(academicLevel, cancellationToken);


                    return ResultDTO.Success(
                       ApplicationResponseConstant.ACADEMIC_LEVEL_UPDATE_SUCCESS_RESPONSE_MESSAGE, academicLevel.Id);
                }
            }
            catch (Exception)
            {
                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE,
                });
            }
        }
    }


}
