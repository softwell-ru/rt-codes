using Microsoft.Extensions.Configuration;
using SoftWell.RtCodes.DependencyInjection;
using SoftWell.RtCodes.Sources.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class CodesConvertionBuilderExtensions
{
    public static ICodesConvertionBuilder AddConfigurationSource(
        this ICodesConvertionBuilder builder,
        IConfiguration configurationSection)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configurationSection);

        builder.Services.AddOptions<CodesMappingConfiguration>()
            .Bind(configurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.AddSource<ConfigurationCodesSource>();

        return builder;
    }
}