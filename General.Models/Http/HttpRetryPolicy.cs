namespace General.Models.Http
{
    public class HttpRetryPolicy
    {
        public int RetryCount { get; set; }

        public double RetryDurationInMiliSeconds { get; set; }
    }
}
