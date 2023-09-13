using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CodesMap = System.Collections.Generic.Dictionary<string, // source schemes
                    System.Collections.Generic.Dictionary<string, // codes in source schemes
                        System.Collections.Generic.Dictionary<string, string>>>; // target scheme: code in target scheme;

namespace SoftWell.RtCodes;

internal sealed class CodesConverter : ICodesConverter, IHostedService
{
    private readonly IReadOnlyList<ICodesSource> _sources;

    private readonly ILogger<CodesConverter> _logger;

    private CodesMap _map = null!;

    public CodesConverter(IEnumerable<ICodesSource> sources, ILogger<CodesConverter> logger)
    {
        _sources = sources?.ToList() ?? throw new ArgumentNullException(nameof(sources));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public ValueTask<string?> ConvertOrDefaultAsync(string code, string sourceScheme, string targetScheme, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(sourceScheme);
        ArgumentNullException.ThrowIfNull(targetScheme);

        if (_map is null) throw new InvalidOperationException("Codes converter was not started");
        if (!_map.TryGetValue(sourceScheme, out var codesInSourceScheme)) return ValueTask.FromResult<string?>(null);
        if (!codesInSourceScheme.TryGetValue(code, out var map)) return ValueTask.FromResult<string?>(null);
        if (!map.TryGetValue(targetScheme, out var res)) return ValueTask.FromResult<string?>(null);

        return ValueTask.FromResult<string?>(res);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return LoadCodesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task LoadCodesAsync(CancellationToken ct)
    {
        _map = new CodesMap(StringComparer.Ordinal);

        _logger.LogDebug("Начинаем загрузку кодов..");

        var schemesCount = 0;
        var codesCount = 0;

        foreach (var s in _sources)
        {
            _logger.LogDebug("Начинаем загрузку кодов из источника {SourceName}..", s.Name);

            await foreach (var c in s.GetCodesAsync(ct))
            {
                if (!_map.TryGetValue(c.SourceScheme, out var scheme))
                {
                    scheme = new Dictionary<string, Dictionary<string, string>>(StringComparer.Ordinal);
                    _map.Add(c.SourceScheme, scheme);
                    schemesCount++;
                }

                if (!scheme.TryGetValue(c.SourceCodeValue, out var codeMap))
                {
                    codeMap = new Dictionary<string, string>(StringComparer.Ordinal);
                    scheme.Add(c.SourceCodeValue, codeMap);
                    codesCount++;
                }

                codeMap[c.TargetScheme] = c.TargetCodeValue;
            }

            _logger.LogDebug("Завершили загрузку кодов из источника {SourceName}.", s.Name);
        }

        _logger.LogDebug("Завершили загрузку кодов. Загружено схем: {SchemesCount}, кодов: {CodesCount}", schemesCount, codesCount);
    }
}
