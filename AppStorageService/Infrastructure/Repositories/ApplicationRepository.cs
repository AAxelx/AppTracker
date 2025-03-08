using AppStorageService.Infrastructure.Contexts;
using AppStorageService.Infrastructure.Entities;
using AppStorageService.Infrastructure.Repositories.Abstractions;
using AppStorageService.Services.DTOs;
using AppStorageService.Services.Enums;
using AppStorageService.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppStorageService.Infrastructure.Repositories;

public class ApplicationRepository(MsSqlDbContext context, IMapper mapper) : IApplicationRepository
{
  public async Task<ApplicationModel?> GetByIdAsync(Guid id)
  {
    var entity = await context.Applications.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    return mapper.Map<ApplicationModel>(entity);
  }

  public async Task<bool> ExistsByUrlAsync(string url) => 
    await context.Applications.AsNoTracking().AnyAsync(a => a.Url == url);

  public async Task<List<ApplicationModel>> GetByStatusAsync(ApplicationStatus status)
  {
    var result = await context.Applications
      .Where(a => a.Status == (int)status)
      .AsNoTracking()
      .Select(a => mapper.Map<ApplicationModel>(a))
      .ToListAsync();

    return result;
  }
  
  public async Task AddAsync(ApplicationModel model)
  {
    await context.Applications.AddAsync(mapper.Map<ApplicationEntity>(model));
    await context.SaveChangesAsync();
  }
  
  public async Task UpdateApplicationStatusAsync(List<UpdateApplicationsStatusDto> dtos)//TODO: upgrade to EFCore.BulkExtensions in case of 1k+ dtos
  {
    var ids = dtos.Select(dto => dto.Id).ToList();
    var dbEntities = await context.Applications
      .Where(a => ids.Contains(a.Id))
      .ToListAsync();

    foreach (var dto in dtos)
    {
      var dbEntity = dbEntities.FirstOrDefault(a => a.Id == dto.Id);
      
      if (dbEntity != null && dbEntity.Status != (int)dto.Status)
      {
        dbEntity.Status = (int)dto.Status;
        context.Entry(dbEntity).Property(a => a.Status).IsModified = true;
      }
    }

    if (context.ChangeTracker.HasChanges())
      await context.SaveChangesAsync();
  }
  
  public async Task DeleteAsync(Guid id)
  {
    context.Applications.Remove(new ApplicationEntity { Id = id });
    await context.SaveChangesAsync();
  }
}