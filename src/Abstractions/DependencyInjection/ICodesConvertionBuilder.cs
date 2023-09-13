using Microsoft.Extensions.DependencyInjection;

namespace SoftWell.RtCodes.DependencyInjection;

public interface ICodesConvertionBuilder
{
    IServiceCollection Services { get; }
}
