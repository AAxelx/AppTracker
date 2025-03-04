using AppStorageService.Services.Enums;

namespace AppStorageService.Services.DTOs;

public record ApplicationDto
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public string Url { get; init; }
  public ApplicationStatus Status { get; set; }
  public StoreType StoreType { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastUpdatedAt { get; set; }
}