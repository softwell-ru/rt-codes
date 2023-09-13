using SoftWell.RtCodes.DependencyInjection;
using SoftWell.RtCodes.Sources.Yaml;

namespace Microsoft.Extensions.DependencyInjection;

public static class CodesConvertionBuilderExtensions
{
    public static ICodesConvertionBuilder AddYamlFileSource(
        this ICodesConvertionBuilder builder,
        string path)
    {
        return builder.AddFileSource(path, YamlCodesStreamReader.Utf8Instance);
    }

    public static ICodesConvertionBuilder AddYamlHttpSource(
        this ICodesConvertionBuilder builder,
        Uri uri,
        Action<HttpClient>? configureClient = null)
    {
        return builder.AddHttpSource(uri, YamlCodesStreamReader.Utf8Instance, configureClient);
    }
}