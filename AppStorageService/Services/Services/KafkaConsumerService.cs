using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;
using AppStorageService.Configurations;
using AppStorageService.Services.DTOs;
using AppStorageService.Services.Handlers.Abstractions;

namespace AppStorageService.Services.Services;

public class KafkaConsumerService : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly KafkaSettings _kafkaSettings;
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IServiceProvider _serviceProvider;
    
    public KafkaConsumerService(
        IOptions<KafkaSettings> kafkaSettings, 
        ILogger<KafkaConsumerService> logger, 
        IServiceProvider serviceProvider)
    {
        _kafkaSettings = kafkaSettings.Value;
        _logger = logger;
        _serviceProvider = serviceProvider;

        var config = new ConsumerConfig
        {
            BootstrapServers = _kafkaSettings.BootstrapServers,
            GroupId = "storage-service-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_kafkaSettings.Topic);

        _logger.LogInformation("Kafka Consumer started, listening for messages...");


        Task.Run(async () =>
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(1));

                    if (consumeResult is null)
                        continue;
                    var message = consumeResult.Message.Value;

                    _logger.LogInformation($"Received Kafka message: {message}");

                    var updateData = JsonSerializer.Deserialize<List<UpdateApplicationsStatusDto>>(message);
                    if (updateData != null)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var applicationStatusHandler =
                            scope.ServiceProvider.GetRequiredService<IApplicationStatusHandler>();
                        await applicationStatusHandler.HandleAsync(updateData);
                    }
                }
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Error while consuming Kafka message: {Message}", ex.Message);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error while deserializing Kafka message: {Message}", ex.Message);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka Consumer is stopping...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating database");
            }
            finally
            {
                _consumer.Close();
            }
        });
    }
}