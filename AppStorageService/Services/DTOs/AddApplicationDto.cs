using System.ComponentModel.DataAnnotations;

namespace AppStorageService.Services.DTOs;

public class AddApplicationDto
{
  public string Name { get; init; }
  [Required(ErrorMessage = "Url cannot be null or empty")]
  public string Url { get; init; }
}