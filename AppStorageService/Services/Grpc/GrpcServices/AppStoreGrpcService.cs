using AppStorageService.Services.Grpc.GrpcServices.Abstractions;
using AppStorageService.Services.Models;
using AutoMapper;
using Grpc.Core;

namespace AppStorageService.Services.Grpc.GrpcServices;

public class AppStoreGrpcService(AppStoreGrpc.AppStoreService.AppStoreServiceClient client, IMapper mapper) : IAppStoreGrpcService
{
  public async Task SendApplicationAsync(ApplicationModel model)
  {
    try
    {
      var response = await client.RegisterApplicationAsync(mapper.Map<AppStoreGrpc.ApplicationRequest>(model));

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