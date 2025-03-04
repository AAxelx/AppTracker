using AppStorageService.Infrastructure.Contexts;
using AppStorageService.Infrastructure.Entities;
using AppStorageService.Infrastructure.Repositories.Abstractions;
using AppStorageService.Services.Enums;
using AppStorageService.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppStorageService.Infrastructure.Repositories;

public class ApplicationRepository(MsSqlDbContext context, IMapper mapper) : IApplicationRepository
{
  public async Task<List<ApplicationModel>> GetByStatusAsync(ApplicationStatus status)
  {
    var applications = await context.Applications
      .Where(a => a.Status == (int)status)
      .ToListAsync();

    var result = applications.Select(mapper.Map<ApplicationModel>).ToList();

    return result;
  }

  public async Task AddAsync(ApplicationModel model)
  {
    await context.Applications.AddAsync(mapper.Map<ApplicationEntity>(model));
    await context.SaveChangesAsync();
  }

  public async Task DeleteAsync(Guid id)
  {
    context.Applications.Remove(new ApplicationEntity { Id = id });
    await context.SaveChangesAsync();
  }
}