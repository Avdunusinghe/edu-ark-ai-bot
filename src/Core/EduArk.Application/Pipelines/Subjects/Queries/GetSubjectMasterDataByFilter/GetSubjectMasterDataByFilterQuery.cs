using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Subjects.Queries.GetSubjectMasterDataByFilter
{
    public record GetSubjectMasterDataByFilterQuery : IRequest<List<DropDownDTO>>
    {
        public string SearchText { get; set; }
    }

    public class GetSubjectMasterDataByFilterQueryHandler : IRequestHandler<GetSubjectMasterDataByFilterQuery, List<DropDownDTO>>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;
        public GetSubjectMasterDataByFilterQueryHandler(ISubjectQueryRepository subjectQueryRepository)
        {
            this._subjectQueryRepository = subjectQueryRepository;
        }
        public async Task<List<DropDownDTO>> Handle(GetSubjectMasterDataByFilterQuery request, CancellationToken cancellationToken)
        {

            var query = await _subjectQueryRepository.Query(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x=>x.Name.Contains(request.SearchText));
            }

            query = query.OrderBy(x => x.Name).Take(10);

            return  query.Select(x=> new DropDownDTO()
            {
                Id = x.Id,
                Name = x.Name,

            }).ToList();
        }
    }
}
