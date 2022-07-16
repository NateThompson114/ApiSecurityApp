using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MonitoringApi.HealthChecks
{
    public class RandomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var responseTimeInMs = Random.Shared.Next(300);

            return responseTimeInMs switch {
                < 100 => Task.FromResult(HealthCheckResult.Healthy($"The response time is excellent ({responseTimeInMs}ms)")),
                
                < 200 => Task.FromResult(HealthCheckResult.Degraded($"The response time is greater than expected ({responseTimeInMs}ms)")),

                _ => Task.FromResult(HealthCheckResult.Unhealthy($"The response time is unacceptable ({responseTimeInMs}ms)"))
            };
        }
    }
}
