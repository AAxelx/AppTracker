using AppStorageService.Services.Grpc.GrpcServices.Abstractions;
using AppStorageService.Services.Models;
using AutoMapper;
using Grpc.Core;

namespace AppStorageService.Services.Grpc.GrpcServices;

public class AppStoreGrpcService(AppStoreGrpc.AppStoreTrackingGrpcService.AppStoreTrackingGrpcServiceClient client, 
  IMapper mapper) : IAppStoreGrpcService
{
  public async Task AddTrackingApplicationAsync(ApplicationModel model)
  {
    try
    {
      var response = await client.AddApplicationTrackingAsync(mapper.Map<AppStoreGrpc.AddApplicationRequest>(model));

      if (!response.Success)
      {
        throw new ApplicationException($"gRPC Error: {response.Message}");
      }
    }
    catch (RpcException ex)
    {
      string errorMessage = $"gRPC request failed. StatusCode: {ex.StatusCode}, Detail: {ex.Status.Detail}";

      throw new Exception(errorMessage, ex);
    }
  }
  
  public async Task RemoveApplicationTrackingAsync(string id)
  {
    try
    {
      var response = await client.RemoveApplicationTrackingAsync(new AppStoreGrpc.RemoveApplicationRequest { Id = id });

      if (!response.Success)
      {
        throw new ApplicationException($"gRPC Error: {response.Message}");
      }
    }
    catch (RpcException ex)
    {
      string errorMessage = $"gRPC request failed. StatusCode: {ex.StatusCode}, Detail: {ex.Status.Detail}";

      throw new Exception(errorMessage, ex);
    }
  }
}