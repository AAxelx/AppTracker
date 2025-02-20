using AppStorageService.Infrastructure.Contexts;
using AppStorageService.Infrastructure.Entities;
using AppStorageService.Infrastructure.Repositories.Abstractions;
using AppStorageService.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppStorageService.Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
  private readonly MsSqlDbContext _context;
  private readonly IMapper _mapper;

  public ApplicationRepository(MsSqlDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<List<ApplicationModel>> GetAllAsync()
  {
    var entities = await _context.Set<ApplicationEntity>().AsNoTracking().ToListAsync();
    return _mapper.Map<List<ApplicationModel>>(entities);
  }

  public async Task AddAsync(ApplicationModel model)
  {
    var entity = _mapper.Map<ApplicationEntity>(model);
    
    await _context.Set<ApplicationEntity>().AddAsync(entity);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(Guid id)
  {
    _context.Set<ApplicationEntity>().Remove(new ApplicationEntity { Id = id });
    await _context.SaveChangesAsync();
  }
}
