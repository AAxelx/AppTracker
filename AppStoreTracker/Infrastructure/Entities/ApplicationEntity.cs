namespace AppStoreTracker.Infrastructure.Entities;

public class ApplicationEntity
{
  public Guid Id { get; set; }
  public string Url { get; set; }
  public int Status { get; set; }
  public DateTime LastUpdatedAt { get; set; }
}