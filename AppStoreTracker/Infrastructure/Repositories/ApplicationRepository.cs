using System.Net.Mime;
using System.Text;
using AppStoreTracker.Infrastructure.Contexts;
using AppStoreTracker.Infrastructure.Entities;
using AppStoreTracker.Infrastructure.Repositories.Abstractions;
using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Models;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AppStoreTracker.Infrastructure.Repositories;

public class ApplicationRepository(MsSqlDbContext dbContext, IMapper mapper) : IApplicationRepository
{
  public async Task<List<ApplicationStatusDto>> GetAllApplicationsStatusAsync(CancellationToken stoppingToken)
  {
    var sql = "SELECT Id, Url, Status, LastUpdatedAt FROM Applications";

    var applications = await dbContext.Applications
      .FromSqlRaw(sql)
      .Select(app => new ApplicationEntity
      {
        Id = app.Id,
        Url = app.Url,
        Status = app.Status,
      })
      .ToListAsync(stoppingToken);

    return mapper.Map<List<ApplicationStatusDto>>(applications);
  }
  
  public async Task<bool> AddAsync(ApplicationModel application)
  {
    var sql = "INSERT INTO Applications (Id, Url, Status, LastUpdatedAt) " +
              "VALUES (@Id, @Url, @Status, @LastUpdatedAt);";

    var parameters = new[]
    {
      new SqlParameter("@Id", application.Id),
      new SqlParameter("@Url", application.Url),
      new SqlParameter("@Status", (int)application.Status),
      new SqlParameter("@LastUpdatedAt", application.LastUpdatedAt)
    };

    var result = await dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
    return result > 0;
  }

  public async Task UpdateApplicationsStatusAsync(List<UpdateApplicationsStatusDto> updateDtos, CancellationToken stoppingToken)
  {
    var parameters = new List<SqlParameter>();
    var sqlBuilder = new StringBuilder();
    var nowUtc = DateTime.UtcNow;

    sqlBuilder.AppendLine("UPDATE Applications SET ");

    sqlBuilder.AppendLine("Status = CASE ");

    for (int i = 0; i < updateDtos.Count; i++)
    {
      var dto = updateDtos[i];
      sqlBuilder.AppendLine($"WHEN Id = @Id_{i} THEN @Status_{i}");
      parameters.Add(new SqlParameter($"@Id_{i}", dto.Id));
      parameters.Add(new SqlParameter($"@Status_{i}", (int)dto.Status));
    }

    sqlBuilder.AppendLine("ELSE Status END,");

    sqlBuilder.AppendLine("LastUpdatedAt = CASE ");

    for (int i = 0; i < updateDtos.Count; i++)
    {
      var dto = updateDtos[i];
      sqlBuilder.AppendLine($"WHEN Id = @Id_{i} THEN @LastUpdatedAt_{i}");
      parameters.Add(new SqlParameter($"@LastUpdatedAt_{i}", nowUtc));
    }

    sqlBuilder.AppendLine("ELSE LastUpdatedAt END ");

    sqlBuilder.AppendLine("WHERE Id IN (" + string.Join(",", updateDtos.Select((dto, index) => $"@Id_{index}")) + ");");

    await dbContext.Database.ExecuteSqlRawAsync(sqlBuilder.ToString(), parameters.ToArray(), stoppingToken);
  }
  
  public async Task<bool> RemoveAsync(Guid id)
  {
    var sql = "DELETE FROM Applications WHERE Id = @Id;";

    var parameters = new[]
    {
      new SqlParameter("@Id", id)
    };

    var result = await dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
    return result > 0;
  }
}