namespace AppStorageService.Services.Models;

public class ApplicationModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string Url { get; set; } = null!;
  public int Status { get; set; }
  public int StoreType { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastUpdatedAt { get; set; }
}