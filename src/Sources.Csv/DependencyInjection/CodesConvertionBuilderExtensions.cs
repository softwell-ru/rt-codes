using SoftWell.RtCodes.DependencyInjection;
using SoftWell.RtCodes.Sources.Csv;

namespace Microsoft.Extensions.DependencyInjection;

public static class CodesConvertionBuilderExtensions
{
    public static ICodesConvertionBuilder AddCsvFileSource(
        this ICodesConvertionBuilder builder,
        string path)
    {
        return builder.AddFileSource(path, CsvCodesStreamReader.Utf8Instance);
    }

    public static ICodesConvertionBuilder AddCsvHttpSource(
        this ICodesConvertionBuilder builder,
        Uri uri,
        Action<HttpClient>? configureClient = null)
    {
        return builder.AddHttpSource(uri, CsvCodesStreamReader.Utf8Instance, configureClient);
    }
}