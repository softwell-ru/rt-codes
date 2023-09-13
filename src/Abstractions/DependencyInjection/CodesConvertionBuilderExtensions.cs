using SoftWell.RtCodes;
using SoftWell.RtCodes.DependencyInjection;
using SoftWell.RtCodes.Sources;

namespace Microsoft.Extensions.DependencyInjection;

public static class CodesConvertionBuilderExtensions
{
    public static ICodesConvertionBuilder AddSource<TSource>(this ICodesConvertionBuilder builder)
        where TSource : class, ICodesSource
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<ICodesSource, TSource>();
        return builder;
    }

    public static ICodesConvertionBuilder AddSource(this ICodesConvertionBuilder builder, ICodesSource source)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(source);

        builder.Services.AddSingleton(source);
        return builder;
    }

    public static ICodesConvertionBuilder AddSource(this ICodesConvertionBuilder builder, Func<IServiceProvider, ICodesSource> sourceFactory)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(sourceFactory);

        builder.Services.AddSingleton(sourceFactory);
        return builder;
    }

    public static ICodesConvertionBuilder AddFileSource(
        this ICodesConvertionBuilder builder,
        string path,
        ICodesStreamReader streamReader)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(streamReader);

        builder.AddSource(
            sp => new FileCodesSource(
                path,
                streamReader));

        return builder;
    }

    public static ICodesConvertionBuilder AddHttpSource(
        this ICodesConvertionBuilder builder,
        Uri uri,
        ICodesStreamReader streamReader,
        Action<HttpClient>? configureClient = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(uri);
        ArgumentNullException.ThrowIfNull(streamReader);

        var uniqueName = uri.ToString();

        builder.Services.AddHttpClient(
            uniqueName,
            configureClient ?? (_ => { }));

        builder.AddSource(
            sp => new HttpCodesSource(
                () => sp.GetRequiredService<IHttpClientFactory>().CreateClient(uniqueName),
                uri,
                streamReader));

        return builder;
    }
}