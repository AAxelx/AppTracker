using AppStorageService.Services.Enums;

namespace AppStorageService.Services.Models;

public class ApplicationModel
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Url { get; set; } = null!;
  public ApplicationStatus Status { get; set; }
  public StoreType StoreType { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastUpdatedAt { get; set; }
}