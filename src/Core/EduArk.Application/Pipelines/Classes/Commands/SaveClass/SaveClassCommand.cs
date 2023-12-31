using EduArk.Application.Common.Constants;
using EduArk.Application.Common.Extensions;
using EduArk.Application.Common.Interfaces;
using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.Classes.Commands.SaveClass
{
    public record SaveClassCommand(ClassDTO classDetails) : IRequest<ResultDTO>
    {
    }

    public class SaveClassCommandHandler : IRequestHandler<SaveClassCommand, ResultDTO>
    {
        private readonly IClassQueryRepository _classQueryRepository;
        private readonly IClassCommandRepository _classCommandRepository;
        private readonly ICurrentUserService _currentUserService;

        public SaveClassCommandHandler(IClassQueryRepository classQueryRepository, IClassCommandRepository classCommandRepository, ICurrentUserService currentUserService)
        {
            this._classQueryRepository = classQueryRepository;
            this._classCommandRepository = classCommandRepository;
            this._currentUserService = currentUserService;
        }

        public async Task<ResultDTO> Handle(SaveClassCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var classObj = (await _classQueryRepository
                               .Query(x => x.AcademicLevelId == request.classDetails.AcademicLevelId &&
                               x.AcademicYearId == request.classDetails.AcademicYearId &&
                               x.ClassNameId == request.classDetails.ClassNameId))
                               .FirstOrDefault();

                if (classObj is null)
                {
                    classObj = request.classDetails.ToEntity();
                    classObj.CreatedByUserId = _currentUserService.UserId!.Value;
                    classObj.CreatedDate = DateTime.UtcNow;
                    classObj.UpdatedByUserId = _currentUserService.UserId.Value;
                    classObj.UpdateDate = DateTime.UtcNow;

                    AddNewClassTeacher(classObj, request.classDetails.ClassTeacherId);

                    AddNewClassSubjectTeachers(classObj, request.classDetails.ClassSubjectTeachers);

                    await _classCommandRepository.AddAsync(classObj, cancellationToken);

                    return ResultDTO.Success(ApplicationResponseConstant.CLASS_SAVE_SUCCESS_RESPONSE_MESSAGE);

                }
                else
                {
                    classObj = request.classDetails.ToEntity(classObj);
                    classObj.UpdatedByUserId = _currentUserService.UserId.Value;
                    classObj.UpdateDate = DateTime.UtcNow;

                    if (classObj.ClassTeachers.Count() > 0)
                    {
                        var classTeacher = classObj.ClassTeachers.FirstOrDefault();

                        classTeacher.TeacherId = request.classDetails.ClassTeacherId;
                    }
                    else
                    {
                        AddNewClassTeacher(classObj, request.classDetails.ClassTeacherId);
                    }

                    var exsistingClassSubjectTeachers = classObj.ClassSubjectTeachers
                                                        .Where(x => x.IsActive)
                                                        .ToList();

                    var newlyAddedClassSubjectTeacher = (from n in request.classDetails.ClassSubjectTeachers where !exsistingClassSubjectTeachers.Any(x => x.Id == n.Id) select n).ToList();

                    // # TODO - need to think relationship to deactive class teacher
                    var deletedClassSubjectTeachers = (from d in exsistingClassSubjectTeachers where !request.classDetails.ClassSubjectTeachers.Any(x => x.Id == d.Id) select d).ToList();

                    AddNewClassSubjectTeachers(classObj, newlyAddedClassSubjectTeacher);

                    await _classCommandRepository.UpdateAsync(classObj, cancellationToken);


                    return ResultDTO.Success(ApplicationResponseConstant.CLASS_UPDATE_SUCCESS_RESPONSE_MESSAGE);
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

        private void AddNewClassSubjectTeachers(Class classObj, List<ClassSubjectTeacherDTO> classSubjectTeachers)
        {
            foreach (var item in classSubjectTeachers)
            {
                classObj.ClassSubjectTeachers.Add(new ClassSubjectTeacher()
                {

                    SubjectId = item.SubjectId,
                    SubjectTeacherId = item.SubjectTeacherId,
                    StartDate = DateTime.UtcNow,
                    IsActive = true,
                   
                });
            }
        }

        private void AddNewClassTeacher(Class classObj, int classTeacherId)
        {
            classObj.ClassTeachers.Add(new Domain.Entities.Tenant.ClassTeacher()
            {
                TeacherId = classTeacherId,
                IsActive = true,
                IsPrimary = true,
                CreatedByUserId = _currentUserService.UserId!.Value,
                CreatedOn = DateTime.UtcNow,
                UpdatedByUserId = _currentUserService.UserId.Value,
                UpdatedOn = DateTime.UtcNow,
        });
        }
    }
}
