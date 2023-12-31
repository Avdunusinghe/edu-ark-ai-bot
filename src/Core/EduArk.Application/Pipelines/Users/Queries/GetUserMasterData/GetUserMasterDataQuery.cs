using EduArk.Application.Common.Helper;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Enums;
using MediatR;

namespace EduArk.Application.Pipelines.Users.Queries.GetUserMasterData
{
    public record GetUserMasterDataQuery : IRequest<UserMasterDataDTO>
    {
    }

    public class GetUserMasterDataQueryHandler
        : IRequestHandler<GetUserMasterDataQuery, UserMasterDataDTO>
    {
        public async Task<UserMasterDataDTO> Handle(GetUserMasterDataQuery request, CancellationToken cancellationToken)
        {
            var userMasterData = new UserMasterDataDTO();

            userMasterData.Roles = 
                Enum.GetValues(typeof(RoleType)).Cast<RoleType>()
                                    .Select(x => new DropDownDTO() 
                                        { Id = (int)x, 
                                            Name = x.ToString() 
                                        })
                                    .ToList();

            userMasterData.UserActiveStatus =
                Enum.GetValues(typeof(UserActiveStatus)).Cast<UserActiveStatus>()
                                    .Select(x => new DropDownDTO()
                                    {
                                        Id = (int)x,
                                        Name = EnumHelper.GetEnumDescription(x)
                                    })
                                    .ToList();

            return userMasterData;
        }
    }
}
