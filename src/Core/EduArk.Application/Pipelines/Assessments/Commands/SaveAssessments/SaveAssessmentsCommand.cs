using EduArk.Application.Common.Constants;
using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Assessments.Commands.SaveAssessments
{
    public record SaveAssessmentsCommand(AssessmentsDTO Assessments) : IRequest<ResultDTO>;

    public class AssessmentCommondHandler : IRequestHandler<SaveAssessmentsCommand, ResultDTO>
    {
        private readonly IAssessmentQueryRepository _assessmentQueryRepository;
        private readonly IAssessmentCommandRepository _assessmentCommandRepository;


        public AssessmentCommondHandler
            (IAssessmentQueryRepository assessmentQueryRepository,
            IAssessmentCommandRepository assessmentCommandRepository)
        {
            this._assessmentQueryRepository = assessmentQueryRepository;
            this._assessmentCommandRepository = assessmentCommandRepository;
        }

        /// <summary>
        /// Handler methis for SaveLessonCommand
        /// </summary>
        /// <param name="request">SaveLessonCommand</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns>result DTO</returns>
        public async Task<ResultDTO> Handle(SaveAssessmentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var assessmnent = await _assessmentQueryRepository
                    .GetById(request.Assessments.Id, cancellationToken);

                if (assessmnent == null)
                {
                    assessmnent = request.Assessments.ToEntity();

                    await _assessmentCommandRepository.AddAsync(assessmnent, cancellationToken);

                    return ResultDTO
                        .Success(
                                 ApplicationResponseConstant.ASSESSMENTS_SAVE_SUCCESS_RESPONSE_MESSAGE,
                                 assessmnent.Id
                                 );
                }
                else
                {
                    assessmnent = request.Assessments.ToEntity(assessmnent);

                    await _assessmentCommandRepository.UpdateAsync(assessmnent, cancellationToken);

                    return ResultDTO
                            .Success
                            (
                                ApplicationResponseConstant.ASSESSMENTS_SAVE_SUCCESS_RESPONSE_MESSAGE,
                                assessmnent.Id
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

    }
}
