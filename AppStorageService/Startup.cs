using AppStorageService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AppStorageService;

public class Startup(IConfiguration configuration)
{
  private IConfiguration Configuration { get; } = configuration;

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddAuthorization();
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
      {
        Title = "AppStorage API",
        Version = "v1"
      });
    });
    
    services.AddDbContext<MsSqlDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnection")));
  }

  public void Configure(WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppStorage API V1");
        c.RoutePrefix = string.Empty;
      });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.Run();
  }
}