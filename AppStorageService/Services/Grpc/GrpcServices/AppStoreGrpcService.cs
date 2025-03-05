using AppStorageService.Services.Grpc.GrpcServices.Abstractions;
using AppStorageService.Services.Models;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;

namespace AppStorageService.Services.Grpc.GrpcServices;

public class AppStoreGrpcService: IAppStoreGrpcService
{
  
  private readonly AppStoreGrpc.AppStoreTrackingGrpcService.AppStoreTrackingGrpcServiceClient _client;
  private readonly IMapper _mapper;

  public AppStoreGrpcService(IConfiguration configuration, IMapper mapper)
  {
    var grpcUrl = configuration["GrpcSettings:TrackerServiceUrl"];
    var channel = GrpcChannel.ForAddress(grpcUrl);
    _client = new AppStoreGrpc.AppStoreTrackingGrpcService.AppStoreTrackingGrpcServiceClient(channel);
    _mapper = mapper;
  }
  
  public async Task AddTrackingApplicationAsync(ApplicationModel model)
  {
    try
    {
      var response = await _client.AddApplicationTrackingAsync(_mapper.Map<AppStoreGrpc.AddApplicationRequest>(model));

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
      var response = await _client.RemoveApplicationTrackingAsync(new AppStoreGrpc.RemoveApplicationRequest { Id = id });

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