using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace Infra;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddDefaultHttpRetryPolicy<T>(
        this IHttpClientBuilder client
    )
    {
        return client.AddPolicyHandler(
            (serviceProvider, _) =>
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(
                        Backoff.ExponentialBackoff(TimeSpan.FromMilliseconds(100), retryCount: 4),
                        onRetry: (exception, sleepDuration, retryCount, _) =>
                            serviceProvider
                                .GetRequiredService<ILogger<T>>()
                                .LogWarning(
                                    exception.Exception,
                                    "{serviceName} retry #{retryCount}, delaying {delayMs} ms.",
                                    typeof(T).Name,
                                    retryCount,
                                    sleepDuration.TotalMilliseconds
                                )
                    )
        );
    }
}