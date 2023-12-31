using EduArk.Application.Common.Helper;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.SubjectDTOs;
using EduArk.Application.Pipelines.SubjectStreams.Queries;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Subjects.Queries.GetSubjectMasterData
{
    public record GetSubjectMasterDataQuery : IRequest<SubjectMasterDataDTO>
    {
    }

    public class GetSubjectMasterDataQueryHandler : IRequestHandler<GetSubjectMasterDataQuery, SubjectMasterDataDTO>
    {
        private readonly IMediator _mediator;
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly ISubjectQueryRepository _subjectQueryRepository;

        public GetSubjectMasterDataQueryHandler(IMediator mediator, IAcademicLevelQueryRepository academicLevelQueryRepository, ISubjectQueryRepository subjectQueryRepository)
        {
            this._mediator = mediator;
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._subjectQueryRepository = subjectQueryRepository;
        }
        public async Task<SubjectMasterDataDTO> Handle(GetSubjectMasterDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjectmasterData = new SubjectMasterDataDTO();

                subjectmasterData.SubjectStreams.AddRange(await _mediator.Send(new GetSubjectStreamsMasterDataQuery()));

                subjectmasterData.SubjectTypes = Enum.GetValues(typeof(SubjectType)).Cast<SubjectType>()
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = (int)x,
                                                    Name = EnumHelper.GetEnumDescription(x)
                                                })
                                                .ToList();

                subjectmasterData.AcademicLevels.AddRange(
                                                (await _academicLevelQueryRepository
                                                .GetAll(cancellationToken)).Where(x => x.IsActive)
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = x.Id,
                                                    Name = x.Name,
                                                }).ToList());

                subjectmasterData.SubjectCategories = Enum.GetValues(typeof(SubjectCategory)).Cast<SubjectCategory>()
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = (int)x,
                                                    Name = EnumHelper.GetEnumDescription(x)
                                                })
                                                .ToList();

                subjectmasterData.ParentBasketSubjects.AddRange(
                                                (await _subjectQueryRepository.Query(x => x.IsParentBasketSubject && x.IsActive))
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = x.Id,
                                                    Name = x.Name,

                                                }).ToList());

                return subjectmasterData;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
