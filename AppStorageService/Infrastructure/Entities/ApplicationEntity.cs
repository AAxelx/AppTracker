namespace AppStorageService.Infrastructure.Entities;

public class ApplicationEntity
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Url { get; set; }
  public int Status { get; set; }
  public int StoreType { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastUpdatedAt { get; set; }
}