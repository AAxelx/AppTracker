namespace AppStorageService.Configurations;

public record KafkaSettings
{
  public string BootstrapServers { get; init; }
  public string Topic { get; init; }
}