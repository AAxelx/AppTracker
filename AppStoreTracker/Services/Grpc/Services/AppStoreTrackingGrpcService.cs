using AppStoreGrpc;
using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Services.Abstractions;
using AutoMapper;
using Grpc.Core;

namespace AppStoreTracker.Services.Grpc.Services;

public class AppStoreTrackingGrpcService(IApplicationTrackingService applicationTrackingService, IMapper mapper)
  : AppStoreGrpc.AppStoreTrackingGrpcService.AppStoreTrackingGrpcServiceBase
{

  public override async Task<ApplicationResponse> AddApplication(AddApplicationRequest request, ServerCallContext context)
  {
    var success = await applicationTrackingService.AddApplicationAsync(mapper.Map<AddApplicationRequestDto>(request));

    return new ApplicationResponse
    {
      Success = success,
      Message = success ? "Application registered successfully" : "Failed to register application"
    };
  }

  public override async Task<ApplicationResponse> RemoveApplication(RemoveApplicationRequest request, ServerCallContext context)
  {
    var success = await applicationTrackingService.RemoveApplicationAsync(request.Id);

    return new ApplicationResponse
    {
      Success = success,
      Message = success ? "Application removed successfully" : "Failed to remove application"
    };
  }
}