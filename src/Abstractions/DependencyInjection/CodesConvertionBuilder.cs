using Microsoft.Extensions.DependencyInjection;

namespace SoftWell.RtCodes.DependencyInjection;

internal class CodesConvertionBuilder : ICodesConvertionBuilder
{
    public CodesConvertionBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IServiceCollection Services { get; }
}