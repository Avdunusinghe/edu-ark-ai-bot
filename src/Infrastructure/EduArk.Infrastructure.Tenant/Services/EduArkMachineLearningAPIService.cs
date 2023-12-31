using EduArk.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace EduArk.Infrastructure.Tenant.Services
{
    public class EduArkMachineLearningAPIService : IEduArkMachineLearningAPIService
    {
        private readonly ILogger<EduArkMachineLearningAPIService> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public EduArkMachineLearningAPIService
        (
            ILogger<EduArkMachineLearningAPIService> _logger,
            IMediator mediator,
            IConfiguration configuration
        )
        {
            this._logger = _logger;
            this._mediator = mediator;
            this._configuration = configuration;
        }
        public async Task<decimal> StudentPerformanceAnalyzeAsync(List<int> inputData)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var apiUrl = _configuration["MarkPredictionApiUrl"].ToString();


                    // Create a new form content to send the data
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(new { input_data = inputData });

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Make a POST request to the Django API
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        var result = await response.Content.ReadAsStringAsync();
                        return decimal.Parse(result);
                    }
                    else
                    {
                        _logger.LogError($"{response.StatusCode}");
                        return 0;
                    }
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public Task LearningStrategiesAnalyzeAsync()
        {
            throw new NotImplementedException();
        }

        public Task MostSuitedSubjectStreamAnalyzeAsync()
        {
            throw new NotImplementedException();
        }

        public Task PersonalizedAssessmentApproachAnalyzeAsync()
        {
            throw new NotImplementedException();
        }

        
    }
}
