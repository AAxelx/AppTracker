using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Models;

namespace AppStoreTracker.Services.Services.Abstractions;

public interface IKafkaProducerService
{
  Task SendMessageAsync(List<UpdateApplicationsStatusDto> message);
}