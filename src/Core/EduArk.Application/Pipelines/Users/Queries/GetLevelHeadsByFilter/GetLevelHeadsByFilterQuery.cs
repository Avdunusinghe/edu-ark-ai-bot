using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Users.Queries.GetLevelHeadsByFilter
{
    public record GetLevelHeadsByFilterQuery() : IRequest<List<DropDownDTO>>
    {
        public string? Name { get; set; }
    }

    public class GetLevelHeadsByFilterQueryHandler : IRequestHandler<GetLevelHeadsByFilterQuery, List<DropDownDTO>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetLevelHeadsByFilterQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }
        public async Task<List<DropDownDTO>> Handle(GetLevelHeadsByFilterQuery request, CancellationToken cancellationToken)
        {
            var levelHeads = new List<DropDownDTO>();

            if (!string.IsNullOrEmpty(request.Name))
            {
                levelHeads = (await _userQueryRepository.Query(x => x.UserRoles
                            .All(x => x.RoleId == (int)RoleType.LevelHead) &&
                            x.FirstName.Contains(request.Name)))
                            .Take(10)
                            .Select(levelHead=> new DropDownDTO()
                            {
                                Id = levelHead.Id,
                                Name = levelHead.FirstName

                            }).ToList();
            }
            else
            {
                levelHeads = (await _userQueryRepository.Query(x => x.UserRoles.All(x => x.RoleId == (int)RoleType.LevelHead)))
                           .Take(10)
                           .Select(levelHead => new DropDownDTO()
                           {
                               Id = levelHead.Id,
                               Name = levelHead.FirstName

                           }).ToList();
            }

            return levelHeads;
        }
    }
}
