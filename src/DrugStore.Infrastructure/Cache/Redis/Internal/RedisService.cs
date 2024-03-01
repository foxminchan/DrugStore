using System.Text;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DrugStore.Infrastructure.Cache.Redis.Internal;

public sealed class RedisService(IOptions<RedisOptions> options) : IRedisService
{
    private const string GetKeysLuaScript = """
                                                local pattern = ARGV[1]
                                                local keys = redis.call('KEYS', pattern)
                                                return keys
                                            """;

    private const string ClearCacheLuaScript = """
                                                   local pattern = ARGV[1]
                                                   for _,k in ipairs(redis.call('KEYS', pattern)) do
                                                       redis.call('DEL', k)
                                                   end
                                               """;

    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer = new(
        () => ConnectionMultiplexer.Connect(options.Value.GetConnectionString())
    );

    private readonly RedisOptions _redisCacheOption = options.Value;

    private ConnectionMultiplexer ConnectionMultiplexer => _connectionMultiplexer.Value;

    private IDatabase Database
    {
        get
        {
            _connectionLock.Wait();

            try
            {
                return ConnectionMultiplexer.GetDatabase();
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }

    public T GetOrSet<T>(string key, Func<T> valueFactory)
        => GetOrSet($"{_redisCacheOption.Prefix}:{key}", valueFactory,
            TimeSpan.FromSeconds(_redisCacheOption.RedisDefaultSlidingExpirationInSecond));

    public T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan expiration)
    {
        var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";

        Guard.Against.NullOrEmpty(key);

        var cachedValue = Database.StringGet(keyWithPrefix);
        if (!string.IsNullOrEmpty(cachedValue)) return GetByteToObject<T>(cachedValue);

        var newValue = valueFactory();
        if (newValue is { }) Database.StringSet(keyWithPrefix, JsonSerializer.Serialize(newValue), expiration);

        return newValue;
    }

    public T? Get<T>(string key)
    {
        var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";

        Guard.Against.NullOrEmpty(_redisCacheOption.Prefix);

        var cachedValue = Database.StringGet(keyWithPrefix);
        return !string.IsNullOrEmpty(cachedValue)
            ? GetByteToObject<T>(cachedValue)
            : default;
    }

    public T HashGetOrSet<T>(string key, string hashKey, Func<T> valueFactory)
    {
        Guard.Against.NullOrEmpty(key);
        Guard.Against.NullOrEmpty(hashKey);

        var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";
        var value = Database.HashGet(keyWithPrefix, hashKey.ToLower());

        if (!string.IsNullOrEmpty(value)) return GetByteToObject<T>(value);

        if (valueFactory() is { })
            Database.HashSet(keyWithPrefix, hashKey.ToLower(),
                JsonSerializer.Serialize(valueFactory()));

        return valueFactory();
    }

    public IEnumerable<string> GetKeys(string pattern)
        => ((RedisResult[])Database.ScriptEvaluate(GetKeysLuaScript, values: [pattern])!)
            .Where(x => x.ToString().StartsWith(_redisCacheOption.Prefix))
            .Select(x => x.ToString())
            .ToArray();

    public IEnumerable<T> GetValues<T>(string key)
        => Database.HashGetAll($"{_redisCacheOption.Prefix}:{key}").Select(x => GetByteToObject<T>(x.Value));

    public bool RemoveAllKeys(string pattern = "*")
    {
        var succeed = true;

        var keys = GetKeys($"{_redisCacheOption.Prefix}:{pattern}");
        foreach (var key in keys) succeed = Database.KeyDelete(key);

        return succeed;
    }

    public void Remove(string key) => Database.KeyDelete($"{_redisCacheOption.Prefix}:{key}");

    public void Reset()
        => Database.ScriptEvaluate(
            ClearCacheLuaScript,
            values: [_redisCacheOption.Prefix + "*"],
            flags: CommandFlags.FireAndForget);

    private static T GetByteToObject<T>(RedisValue value)
    {
        var result = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value!));
        return result is null ? throw new InvalidOperationException("Deserialization failed.") : result;
    }
}