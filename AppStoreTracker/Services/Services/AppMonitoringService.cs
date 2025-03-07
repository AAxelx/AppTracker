using System.Collections.Concurrent;
using System.Net;
using AppStoreTracker.Configurations;
using AppStoreTracker.Infrastructure.Repositories.Abstractions;
using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Enums;
using AppStoreTracker.Services.Services.Abstractions;
using AutoMapper;
using Microsoft.Extensions.Options;
using ILogger = Serilog.ILogger;

namespace AppStoreTracker.Services.Services;

public class AppMonitoringService(
    IServiceProvider serviceProvider,
    ILogger logger,
    IOptions<AppSettings> appSettings,
    IMapper mapper)
    : BackgroundService
{
    private readonly HttpClient _httpClient = new ();
    private readonly SemaphoreSlim _semaphore = new (appSettings.Value.MaxParallelRequests);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                // var kafkaProducer = scope.ServiceProvider.GetRequiredService<IKafkaProducerService>();
                var applicationRepository = scope.ServiceProvider.GetRequiredService<IApplicationRepository>();
                
                var apps = await applicationRepository.GetAllApplicationsStatusAsync(stoppingToken);
                
                var updatedApplicationDtos = new ConcurrentBag<UpdateApplicationsStatusDto>();
                var tasks = new List<Task>();

                foreach (var app in apps)
                {
                    await _semaphore.WaitAsync(stoppingToken);

                    tasks.Add(MonitorAppStatusAsync(app, stoppingToken, updatedApplicationDtos));
                }

                await Task.WhenAll(tasks);

                if (updatedApplicationDtos.Count > 0)
                {
                    var updatedApplicationModels = updatedApplicationDtos.ToList();
                    
                    // await kafkaProducer.SendMessageAsync(updatedApplicationModels);
                    await applicationRepository.UpdateApplicationsStatusAsync(updatedApplicationModels, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error while monitoring apps");
            }

            await Task.Delay(TimeSpan.FromMinutes(appSettings.Value.CheckIntervalMinutes), stoppingToken);
        }
    }

    private async Task MonitorAppStatusAsync(ApplicationStatusDto app, CancellationToken stoppingToken, ConcurrentBag<UpdateApplicationsStatusDto> updates)
    {
        var response = await _httpClient.GetAsync(app.Url, stoppingToken);
        var currentStatus = response.StatusCode == HttpStatusCode.NotFound ? ApplicationStatus.Deleted : ApplicationStatus.Active;

        if (app.Status != currentStatus)
        {
            app.Status = currentStatus;
            updates.Add(mapper.Map<UpdateApplicationsStatusDto>(app));
        }

        _semaphore.Release();
    }
}