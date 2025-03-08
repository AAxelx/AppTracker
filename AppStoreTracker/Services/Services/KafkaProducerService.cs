using System.Text.Json;
using AppStoreTracker.Configurations;
using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Services.Abstractions;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using ILogger = Serilog.ILogger;

namespace AppStoreTracker.Services.Services;

public class KafkaProducerService : IKafkaProducerService
{
  private readonly IProducer<string, string> _producer;
  private readonly ILogger _logger;
  private readonly KafkaSettings _kafkaSettings;

  public KafkaProducerService(IOptions<KafkaSettings> kafkaSettings, ILogger logger)
  {
    _kafkaSettings = kafkaSettings.Value;
    var config = new ProducerConfig { BootstrapServers = _kafkaSettings.BootstrapServers };
    _producer = new ProducerBuilder<string, string>(config).Build();
    _logger = logger;
  }

  public async Task SendMessageAsync(List<UpdateApplicationsStatusDto> message)
  {
    var jsonMessage = JsonSerializer.Serialize(message);

    try
    {
      _logger.Information($"Sending message to Kafka: {jsonMessage}");

      await _producer.ProduceAsync(_kafkaSettings.Topic, new Message<string, string> { Value = jsonMessage });

      _logger.Information($"Message sent to Kafka: {jsonMessage}");
    }
    catch (Exception ex)
    {
      _logger.Error(ex, "Error while sending message to Kafka");
    }
  }
}