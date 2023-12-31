using EduArk.Application.Common.Helper;
using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Enums;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Classes.Queries.GetClassMasterData
{
    public record GetClassMasterDataQuery() : IRequest<ClassMasterDataDTO>
    {
    }

    public class GetClassMasterDataQueryHandler :IRequestHandler<GetClassMasterDataQuery, ClassMasterDataDTO>
    {
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IAcademicLevelQueryRepository _academicLevelQueryRepository;
        private readonly IClassNameQueryRepository _classNameQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        

        public GetClassMasterDataQueryHandler
        (
            IAcademicYearQueryRepository academicYearQueryRepository, 
            IAcademicLevelQueryRepository academicLevelQueryRepository, 
            IClassNameQueryRepository classNameQueryRepository, 
            IUserQueryRepository userQueryRepository
           
        )
        {
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._academicLevelQueryRepository = academicLevelQueryRepository;
            this._classNameQueryRepository = classNameQueryRepository;
            this._userQueryRepository = userQueryRepository;
          
        }
        public async Task<ClassMasterDataDTO> Handle(GetClassMasterDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var classMasterData = new ClassMasterDataDTO();

                classMasterData.CurrentAcademicYear = (await _academicYearQueryRepository.Query(x => x.IsActive == true && x.IsCurrentYear == true)).FirstOrDefault()!.Id;

                classMasterData.AcademicYears = (await _academicYearQueryRepository.Query(x => x.IsActive == true))
                                                .OrderBy(x => x.Id)
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = x.Id,
                                                    Name = x.Id.ToString()

                                                }).ToList();

                classMasterData.ClassNames = (await _classNameQueryRepository
                                            .Query(x => x.IsActive == true))
                                            .OrderBy(x => x.Name).Select(x => new DropDownDTO()
                                            {
                                                Id = x.Id,
                                                Name = x.Name

                                            }).ToList();

                classMasterData.AcademicLevels = (await _academicLevelQueryRepository
                                                .Query(x => x.IsActive == true))
                                                .OrderBy(x => x.Name)
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = x.Id,
                                                    Name = x.Name

                                                }).ToList();

                classMasterData.AllTeachers = (await _userQueryRepository
                                              .Query(x => x.UserRoles.Any(x => x.RoleId == 5)))
                                              .Select(x=> new DropDownDTO()
                                              {
                                                  Id = x.Id,
                                                 Name = $"{x.FirstName} {x.LastName}",

                                              }).ToList();

                classMasterData.ClassCategories = Enum.GetValues(typeof(ClassCategory)).Cast<ClassCategory>()
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = (int)x,
                                                    Name = EnumHelper.GetEnumDescription(x)
                                                })
                                                .ToList();

                classMasterData.LanguageStreams = Enum.GetValues(typeof(LanguageStream)).Cast<LanguageStream>()
                                                .Select(x => new DropDownDTO()
                                                {
                                                    Id = (int)x,
                                                    Name = EnumHelper.GetEnumDescription(x)
                                                })
                                                .ToList();

              

                return classMasterData;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
