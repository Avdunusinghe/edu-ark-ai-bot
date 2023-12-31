using EduArk.Application.Common.Constants;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Application.Pipelines.ClassNames.Commands.DeleteClassName
{
    public record DeleteClassNameCommand(int id) : IRequest<ResultDTO>
    {
    }

    public class DeleteClassNameCommandHandler : IRequestHandler<DeleteClassNameCommand, ResultDTO>
    {
        private readonly IClassNameQueryRepository _classNameQueryRepository;
        private readonly IClassNameCommandRepository _classNameCommandRepository;

        public DeleteClassNameCommandHandler(IClassNameQueryRepository classNameQueryRepository, IClassNameCommandRepository classNameCommandRepository)
        {
            this._classNameQueryRepository = classNameQueryRepository;
            this._classNameCommandRepository = classNameCommandRepository;
        }
        public async Task<ResultDTO> Handle(DeleteClassNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var className = await _classNameQueryRepository
                                .GetById(request.id, cancellationToken);

                if(className is null)
                {
                    return ResultDTO.Failure(new List<string>()
                    {
                        ApplicationResponseConstant.CLASS_NAME_NOT_AVAILABLE_RESPONSE_MESSAGE
                    });
                }
                else
                {
                    className.IsActive = false;

                    await _classNameCommandRepository.UpdateAsync(className, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.CLASS_NAME_SAVE_SUCCESS_RESPONSE_MESSAGE);

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
