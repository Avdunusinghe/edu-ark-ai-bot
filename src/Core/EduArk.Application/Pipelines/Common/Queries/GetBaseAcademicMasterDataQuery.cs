using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Common.Queries
{
    public record GetBaseAcademicMasterDataQuery() : IRequest<BaseAcademicMasterDataDTO>
    {
    }

    public class GetBaseAcademicMasterDataQueryHandler : IRequestHandler<GetBaseAcademicMasterDataQuery, BaseAcademicMasterDataDTO>
    {
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly ISemesterQueryRepository _semesterQueryRepository;
        private readonly IExamTypeQueryRepository _examTypeQueryRepository;
        public GetBaseAcademicMasterDataQueryHandler
        (
            IAcademicYearQueryRepository academicYearQueryRepository,
            IAcademicLevelQueryRepository academicLevelQueryRepository,
            ISemesterQueryRepository semesterQueryRepository,
            IExamTypeQueryRepository examTypeQueryRepository
        )
        {
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._semesterQueryRepository = semesterQueryRepository;
            this._examTypeQueryRepository = examTypeQueryRepository;
        }
        public async Task<BaseAcademicMasterDataDTO> Handle(GetBaseAcademicMasterDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var baseAcademicMasterData = new BaseAcademicMasterDataDTO();

                baseAcademicMasterData.CurrentAcademicYear = (await _academicYearQueryRepository
                                                            .Query(x => x.IsActive == true && x.IsCurrentYear == true))
                                                            .FirstOrDefault()!.Id;

                baseAcademicMasterData.AcademicYears = (await _academicYearQueryRepository.Query(x => x.IsActive == true))
                                                .OrderBy(x => x.Id)
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = x.Id,
                                                    Name = x.Id.ToString(),

                                                }).ToList();

                baseAcademicMasterData.AcademicLevels = (await _academicLevelQueryRepository
                                               .Query(x => x.IsActive == true))
                                               .OrderBy(x => x.Name)
                                               .Select(x => new DropDownDTO()
                                               {
                                                   Id = x.Id,
                                                   Name = x.Name

                                               }).ToList();

                baseAcademicMasterData.Semesters = (await _semesterQueryRepository.GetAll(cancellationToken)).Select(x => new DropDownDTO()
                                                    {
                                                        Id= x.Id,
                                                        Name = x.Name,
                                                    }).ToList();

                baseAcademicMasterData.ExamTypes = (await _examTypeQueryRepository.GetAll(cancellationToken)).Select(x => new DropDownDTO()
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                    }).ToList();

                return baseAcademicMasterData;
            }
            catch (Exception ex)
            {
                return new BaseAcademicMasterDataDTO();
            }
        }
    }
}
