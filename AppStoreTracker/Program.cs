using AppStoreTracker.Services.Services;
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
        options.Configure(context.Configuration.GetSection("Kestrel"));
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
    }
    finally
    {
      Log.CloseAndFlush();
    }
  }
}