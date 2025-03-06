using AppStoreTracker.Services.Grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace AppStoreTracker;

public class Program
{
  public static void Main(string[] args)
  {
    Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.Console()
      .CreateLogger();

    try
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Logging.AddSerilog();

      builder.WebHost.ConfigureKestrel((context, options) =>
      {
        var port = 5200;
        options.ListenAnyIP(port, listenOptions =>
        {
          listenOptions.UseHttps(httpsOptions =>
          {
            httpsOptions.ServerCertificate = context.Configuration.GetSection("Kestrel:Certificates:Development").Get<System.Security.Cryptography.X509Certificates.X509Certificate2>();
          });
          listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        });
      });
      
      var startup = new Startup(builder.Configuration);
      startup.ConfigureServices(builder.Services);
      var app = builder.Build();
      startup.Configure(app, app.Environment);

      app.Run();
    }
    catch (Exception ex)
    {
      Log.Fatal(ex, "Application start-up failed");
      throw;
    }
    finally
    {
      Log.CloseAndFlush();
    }
  }
}