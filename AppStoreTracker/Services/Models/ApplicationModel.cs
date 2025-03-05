using AppStoreTracker.Services.Enums;

namespace AppStoreTracker.Services.Models;

public class ApplicationModel
{
  public Guid Id { get; set; }
  public string Url { get; set; }
  public ApplicationStatus Status { get; set; }
  public DateTime LastUpdatedAt { get; set; }
}