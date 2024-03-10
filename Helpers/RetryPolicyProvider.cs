using Polly;

namespace SubsApi.Helpers
{
    public class RetryPolicyProvider
    {
        //On Exception, retry 3 times, wait one second after each retry
        public static Policy GetRetryPolicy(int retryCount = 3, int sleepDurationInSeconds = 1)
        {
            return Policy
                .Handle<Exception>() // Retry on any exception
                .WaitAndRetry(retryCount, attempt => TimeSpan.FromSeconds(attempt * sleepDurationInSeconds));
        }
        public static AsyncPolicy GetAsyncRetryPolicy(int retryCount = 3)
        {
            return Policy
                .Handle<Exception>() // Retry on any exception
                .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
