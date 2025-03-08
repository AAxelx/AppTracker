using AppStorageService.Services.Grpc.GrpcServices.Abstractions;
using AppStorageService.Services.Models;
using AutoMapper;
using Grpc.Core;

namespace AppStorageService.Services.Grpc.GrpcServices;

public class GooglePlayGrpcService(GooglePlayGrpc.GooglePlayService.GooglePlayServiceClient client, IMapper mapper) : IGooglePlayGrpcService
{
  public async Task AddTrackingApplicationAsync(ApplicationModel model)
  {
    try
    {
      var response = await client.RegisterApplicationAsync(mapper.Map<GooglePlayGrpc.ApplicationRequest>(model));

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