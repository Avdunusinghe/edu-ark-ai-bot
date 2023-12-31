using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.StudentDTOs;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using MediatR;

namespace EduArk.Application.Pipelines.StudentTargetSettings.Commads.SaveTeacherTargetScore
{
    public record SaveTeacherTargetScoreCommand(TeacherTargetScoreContainerDTO TeacherTargetScoreContainer) : IRequest<ResultDTO>
    {

    }

    public class SaveTeacherTargetScoreCommandHandler : IRequestHandler<SaveTeacherTargetScoreCommand, ResultDTO>
    {
        private readonly ISubjectTargetSettingQueryRepository _subjectTargetSettingQueryRepository;
        private readonly ISubjectTargetSettingCommandRepository _subjectTargetSettingCommandRepository;
        public SaveTeacherTargetScoreCommandHandler
        (
            ISubjectTargetSettingQueryRepository subjectTargetSettingQueryRepository,
            ISubjectTargetSettingCommandRepository subjectTargetSettingCommandRepository
        )
        {
            this._subjectTargetSettingQueryRepository = subjectTargetSettingQueryRepository;
            this._subjectTargetSettingCommandRepository = subjectTargetSettingCommandRepository;
        }
        public async Task<ResultDTO> Handle(SaveTeacherTargetScoreCommand request, CancellationToken cancellationToken)
        {
           

            foreach (var item in request.TeacherTargetScoreContainer.TeacherTargetScores)
            {
                var subjectTargetSetting = await _subjectTargetSettingQueryRepository.GetById(item.Id, cancellationToken);

                subjectTargetSetting.TeacherTargetScore = item.TeacherTargetScore;

                await _subjectTargetSettingCommandRepository.UpdateAsync(subjectTargetSetting, cancellationToken);

            }

            return ResultDTO.Success("Teacher target scores has been update successfully.");
        }
    }
}
