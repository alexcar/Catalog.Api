using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Api.HealthChecks
{
    public class RedisCacheHealthCheck { }
    
    //public class RedisCacheHealthCheck : IHealthCheck
    //{
    //    private readonly CacheSettings _settings;

    //    public RedisCacheHealthCheck(IOptions<CacheSettings> settings)
    //    {
    //        _settings = settings.Value;
    //    }

    //    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
    //        CancellationToken cancellationToken = default)
    //    {
    //        try
    //        {
    //            var redis = ConnectionMultiplexer.Connect(_settings.ConnectionString);
    //            var db = redis.GetDatabase();

    //            var result = await db.PingAsync();
    //            if (result < TimeSpan.FromSeconds(5))
    //            {
    //                return await Task.FromResult(
    //                    HealthCheckResult.Healthy());
    //            }

    //            return await Task.FromResult(
    //                HealthCheckResult.Unhealthy());
    //        }
    //        catch (Exception e)
    //        {
    //            return await Task.FromResult(
    //                HealthCheckResult.Unhealthy(e.Message));
    //        }
    //    }

    //    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
