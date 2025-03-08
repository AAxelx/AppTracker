using AppStoreTracker.Configurations;
using AppStoreTracker.Infrastructure.Contexts;
using AppStoreTracker.Infrastructure.Repositories;
using AppStoreTracker.Infrastructure.Repositories.Abstractions;
using AppStoreTracker.Mappings;
using AppStoreTracker.Services.Grpc.Services;
using AppStoreTracker.Services.Services;
using AppStoreTracker.Services.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppStoreTracker;

public class Startup(IConfiguration configuration)
{
  public IConfiguration Configuration { get; } = configuration;

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddGrpc();
    
    services.AddSingleton<Serilog.ILogger>(new LoggerConfiguration()
      .WriteTo.Console()
      .CreateLogger());
    
    services.AddAutoMapper(typeof(ApplicationMappingProfile).Assembly);
    
    services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
    services.Configure<KafkaSettings>(Configuration.GetSection("KafkaSettings"));

    services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
    
    services.AddScoped<AppStoreTrackingGrpcService>();

    services.AddHostedService<AppMonitoringService>();    
    
    services.AddScoped<IApplicationTrackingService, ApplicationTrackingService>();
    
    services.AddScoped<IApplicationRepository, ApplicationRepository>();
    
    services.AddDbContext<MsSqlDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnection")));
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapGrpcService<AppStoreTrackingGrpcService>();
    });
  }
}