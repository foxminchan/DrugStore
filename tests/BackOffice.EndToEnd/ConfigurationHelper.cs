using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace BackOffice.EndToEnd;

public static class ConfigurationHelper
{
    private static readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

    private static readonly string _baseUrl
        = _configuration["BaseUrl"] ?? throw new ConfigurationErrorsException("BaseUrl is not configured.");

    private static readonly int _slowMoMilliseconds = int.Parse(_configuration["SlowMoMilliseconds"] ?? "0");

    private static readonly bool _headless = bool.Parse(_configuration["Headless"] ?? "true");

    public static string GetBaseUrl() => _baseUrl.TrimEnd('/');

    public static int GetSlowMoMilliseconds() => _slowMoMilliseconds;

    public static bool GetHeadless() => _headless;
}