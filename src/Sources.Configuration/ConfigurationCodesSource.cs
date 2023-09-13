using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace SoftWell.RtCodes.Sources.Configuration;

internal class ConfigurationCodesSource : ICodesSource
{
    private readonly CodesMappingConfiguration _config;

    public ConfigurationCodesSource(IOptions<CodesMappingConfiguration> config)
    {
        _config = config?.Value ?? throw new ArgumentNullException(nameof(config));
    }

    public string Name => nameof(ConfigurationCodesSource);

    public async IAsyncEnumerable<Code> GetCodesAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await Task.Yield();

        foreach (var s in _config.Schemes)
        {
            foreach (var c in s.Codes)
            {
                foreach (var m in c.Mapping)
                {
                    ct.ThrowIfCancellationRequested();

                    yield return new Code
                    {
                        SourceScheme = s.Scheme,
                        SourceCodeValue = c.Code,
                        TargetScheme = m.Scheme,
                        TargetCodeValue = m.Code
                    };
                }
            }
        }
    }
}