using EduArk.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduArk.Application.Common.Behaviours
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly ITenantDbContext _tenantDbContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, ITenantDbContext tenantDbContext)
        {
            _logger = logger;
            _tenantDbContext = tenantDbContext;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                await _tenantDbContext.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation($"Begin Transaction : {typeof(TRequest).Name}");
                    await _tenantDbContext.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _tenantDbContext.CommitTransactionAsync(cancellationToken);
                    _logger.LogInformation($"End transaction : {typeof(TRequest).Name}");
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");
                await _tenantDbContext.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex.Message, ex.StackTrace);

                throw;
            }

            return response;
        }
    }
}
