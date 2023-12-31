using EduArk.Application.Common.Constants;
using EduArk.Application.Common.Extensions;
using EduArk.Application.DTOs.ClassNameDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.ClassNames.Commands.SaveClassName
{
    public record SaveClassNameCommand(ClassNameDTO classNameDetails) : IRequest<ResultDTO>
    {
    }

    public class SaveClassNameCommandHandler : IRequestHandler<SaveClassNameCommand, ResultDTO>
    {
        private readonly IClassNameQueryRepository _classNameQueryRepository;
        private readonly IClassNameCommandRepository _classNameCommandRepository;

        public SaveClassNameCommandHandler(IClassNameQueryRepository classNameQueryRepository, IClassNameCommandRepository classNameCommandRepository)
        {
            this._classNameQueryRepository = classNameQueryRepository;
            this._classNameCommandRepository = classNameCommandRepository;
        }
        public async Task<ResultDTO> Handle(SaveClassNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var className = await _classNameQueryRepository
                                    .GetById(request.classNameDetails.Id, cancellationToken);

                if (className is null) 
                {
                    className = request.classNameDetails.ToEntity();

                    var newClassName = await _classNameCommandRepository
                                            .AddAsync(className, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.CLASS_NAME_SAVE_SUCCESS_RESPONSE_MESSAGE, newClassName.Id);
                }
                else
                {
                    className = request.classNameDetails.ToEntity(className);

                   await _classNameCommandRepository
                                           .UpdateAsync(className, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.CLASS_NAME_UPDATE_SUCCESS_RESPONSE_MESSAGE);
                }
            }
            catch (Exception ex)
            {

                return ResultDTO.Failure(new List<string>()
                {
                    ApplicationResponseConstant.COMMON_EXCEPTION_RESPONSE_MESSAGE
                });
            }
        }
    }
}
