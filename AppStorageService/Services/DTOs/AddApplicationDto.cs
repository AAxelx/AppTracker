using System.ComponentModel.DataAnnotations;

namespace AppStorageService.Services.DTOs;

public class AddApplicationDto
{
  [Required(ErrorMessage = "Name cannot be null or empty")]
  public string Name { get; init; }
  [Required(ErrorMessage = "Url cannot be null or empty")]
  public string Url { get; init; }
}