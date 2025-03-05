using AppStorageService.Services.Enums;

namespace AppStorageService.Services.DTOs;

public record UpdateApplicationsStatusDto
{
  public Guid Id { get; init; }
  public ApplicationStatus Status { get; init; }
}