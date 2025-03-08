namespace AppStoreTracker.Configurations;

public record AppSettings
{
  public int MaxParallelRequests { get; init; }
  public int CheckIntervalMinutes { get; init; }
}