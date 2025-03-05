using AppStoreTracker.Services.Enums;

namespace AppStoreTracker.Services.Dtos;

public class ApplicationStatusDto
{
  public Guid Id { get; init; }
  public string Url { get; init; }
  public ApplicationStatus Status { get; set; }
}