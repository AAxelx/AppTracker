using AppStorageService.Configurations;
using AppStorageService.Infrastructure.Contexts;
using AppStorageService.Infrastructure.Repositories;
using AppStorageService.Infrastructure.Repositories.Abstractions;
using AppStorageService.Mappings;
using AppStorageService.Services.Grpc.GrpcServices;
using AppStorageService.Services.Grpc.GrpcServices.Abstractions;
using AppStorageService.Services.Handlers;
using AppStorageService.Services.Handlers.Abstractions;
using AppStorageService.Services.Services;
using AppStorageService.Services.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AppStorageService;

public class Startup(IConfiguration configuration)
{
  private IConfiguration Configuration { get; } = configuration;

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddCors(options =>
    {
      options.AddPolicy("AllowAllOrigins", policy =>
      {
        policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      });
    });
    
    services.AddAuthorization();
    services.AddControllers();
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
      {
        Title = "AppStorage API",
        Version = "v1"
      });
    });
    
    services.AddAutoMapper(typeof(ApplicationMappingProfile).Assembly);
    
    services.Configure<KafkaSettings>(Configuration.GetSection("KafkaSettings"));

    services.AddHostedService<KafkaConsumerService>();
    
    services.AddGrpc();
    var grpcSettings = Configuration.GetSection("GrpcSettings");
    
    services.AddGrpcClient<AppStoreGrpc.AppStoreTrackingGrpcService.AppStoreTrackingGrpcServiceClient>(o =>
    {
      o.Address = new Uri(grpcSettings["AppStoreServiceUrl"]!);
    });

    services.AddGrpcClient<GooglePlayGrpc.GooglePlayService.GooglePlayServiceClient>(o =>
    {
      o.Address = new Uri(grpcSettings["GooglePlayServiceUrl"]!);
    });
    
    services.AddScoped<IApplicationStatusHandler, ApplicationStatusHandler>();
    
    services.AddScoped<IAppStoreGrpcService, AppStoreGrpcService>();
    services.AddScoped<IGooglePlayGrpcService, GooglePlayGrpcService>();
    
    services.AddDbContext<MsSqlDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnection")));

    services.AddScoped<IApplicationService, ApplicationService>();
    services.AddScoped<IApplicationRepository, ApplicationRepository>();
  }

  public void Configure(WebApplication app)
  {
    app.UseCors("AllowAllOrigins");

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
    app.MapControllers();

    app.Run();
  }
}