using Serilog;

namespace AppStorageService;

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

      var startup = new Startup(builder.Configuration);
      startup.ConfigureServices(builder.Services);

      var app = builder.Build();
      startup.Configure(app);

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