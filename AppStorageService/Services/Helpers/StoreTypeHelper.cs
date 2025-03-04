using AppStorageService.Services.Enums;

namespace AppStorageService.Services.Helpers;

public static class StoreTypeHelper
{
  private static readonly string GooglePlayDomain = "play.google.com";//TODO: move strings to Consts class
  private static readonly string AppStoreDomain = "apps.apple.com";
  public static StoreType GetStoreTypeFromUrl(string url)
  {
    if (string.IsNullOrWhiteSpace(url))
      return StoreType.Unknown;

    return url switch
    {
      _ when url.Contains(GooglePlayDomain) => StoreType.GooglePlay,
      _ when url.Contains(AppStoreDomain) => StoreType.AppStore,
      _ => StoreType.Unknown
    };
  }
}