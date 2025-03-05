using AppStoreTracker.Services.Enums;

namespace AppStoreTracker.Services.Dtos;

public record UpdateApplicationsStatusDto
{
  public Guid Id { get; init; }
  public ApplicationStatus Status { get; init; }
}