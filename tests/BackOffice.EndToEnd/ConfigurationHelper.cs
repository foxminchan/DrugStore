using Microsoft.Extensions.Configuration;

namespace BackOffice.EndToEnd;

public static class ConfigurationHelper
{
    private static readonly IConfiguration _configuration;

    static ConfigurationHelper() =>
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

    private static string? _baseUrl;

    private static int _slowMoMilliseconds;

    private static bool _headless;

    public static string GetBaseUrl()
    {
        if (_baseUrl is not null)
            return _baseUrl;

        _baseUrl = _configuration["BaseUrl"] ?? "https://localhost:7050";

        ArgumentNullException.ThrowIfNull(_baseUrl);

        _baseUrl = _baseUrl.TrimEnd('/');

        return _baseUrl;
    }

    public static int GetSlowMoMilliseconds()
    {
        if (_slowMoMilliseconds != 0)
            return _slowMoMilliseconds;

        _slowMoMilliseconds = int.Parse(_configuration["SlowMoMilliseconds"] ?? "200");

        return _slowMoMilliseconds;
    }

    public static bool GetHeadless()
    {
        if (_headless)
            return _headless;

        _headless = bool.Parse(_configuration["Headless"] ?? "false");

        return _headless;
    }
}