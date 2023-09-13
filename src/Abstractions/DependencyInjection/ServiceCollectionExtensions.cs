using Microsoft.Extensions.Hosting;
using SoftWell.RtCodes;
using SoftWell.RtCodes.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCodesConversion(
        this IServiceCollection services,
        Action<ICodesConvertionBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        var b = new CodesConvertionBuilder(services);
        configure(b);

        services.AddSingleton<CodesConverter>();
        services.AddSingleton<ICodesConverter>(sp => sp.GetRequiredService<CodesConverter>());
        services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<CodesConverter>());

        return services;
    }
}