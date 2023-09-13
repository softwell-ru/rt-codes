using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using CodesMap = System.Collections.Generic.Dictionary<string, // source schemes
                    System.Collections.Generic.Dictionary<string, // codes in source schemes
                        System.Collections.Generic.Dictionary<string, string>>>; // target scheme: code in target scheme;

namespace SoftWell.RtCodes.Sources.Yaml;

public class YamlCodesStreamReader : ICodesStreamReader
{
    public static readonly YamlCodesStreamReader Utf8Instance = new(Encoding.UTF8);

    private readonly IDeserializer _deserializer;

    private readonly Encoding _encoding;

    public YamlCodesStreamReader(Encoding encoding)
    {
        _deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
    }

    public IAsyncEnumerable<Code> ReadCodesAsync(Stream stream, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(stream);

        return ReadCodesInner(stream).ToAsyncEnumerable();
    }

    private IEnumerable<Code> ReadCodesInner(Stream stream)
    {
        using var reader = new StreamReader(stream, _encoding, leaveOpen: true);

        var map = _deserializer.Deserialize<CodesMap>(reader);

        foreach (var scheme in map)
        {
            foreach (var code in scheme.Value)
            {
                foreach (var m in code.Value)
                {
                    yield return new Code
                    {
                        SourceScheme = scheme.Key,
                        SourceCodeValue = code.Key,
                        TargetScheme = m.Key,
                        TargetCodeValue = m.Value
                    };
                }
            }
        }
    }
}