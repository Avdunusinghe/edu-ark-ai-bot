namespace EduArk.Application.Common.Interfaces
{
    public interface IEduArkMachineLearningAPIService
    {
        Task<decimal> StudentPerformanceAnalyzeAsync(List<int> inputData);
        Task LearningStrategiesAnalyzeAsync();
        Task PersonalizedAssessmentApproachAnalyzeAsync();
        Task MostSuitedSubjectStreamAnalyzeAsync();
    }
}
