namespace AppStoreTracker.Services.Dtos;

public record AddApplicationRequestDto
{
  public string Id { get; init; }
  public string Url { get; init; }
  public string Status { get; init; }
}