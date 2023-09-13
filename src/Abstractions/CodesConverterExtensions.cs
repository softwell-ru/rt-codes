namespace SoftWell.RtCodes;

public static class CodesConverterExtensions
{
    public static async ValueTask<string> ConvertAsync(
        this ICodesConverter converter,
        string code,
        string sourceScheme,
        string targetScheme,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(converter);
        return await converter.ConvertOrDefaultAsync(code, sourceScheme, targetScheme, ct)
            ?? throw new InvalidOperationException($"Cannot convert code '{code}' from scheme '{sourceScheme}' to '{targetScheme}'");
    }

    public static async ValueTask<string> ConvertRtPartyIdAsync(
        this ICodesConverter converter,
        string partyId,
        string targetScheme,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(converter);
        ArgumentNullException.ThrowIfNull(partyId);
        ArgumentNullException.ThrowIfNull(targetScheme);

        return await converter.ConvertAsync(partyId, RtSchemes.PartyId, targetScheme, ct);
    }

    public static ValueTask<string> ConvertRtProductIdAsync(
        this ICodesConverter converter,
        string productId,
        string targetScheme,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(converter);
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentNullException.ThrowIfNull(targetScheme);

        return converter.ConvertAsync(productId, RtSchemes.ProductId, targetScheme, ct);
    }

    public static ValueTask<string?> ConvertRtPartyIdOrDefaultAsync(
        this ICodesConverter converter,
        string partyId,
        string targetScheme,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(converter);
        ArgumentNullException.ThrowIfNull(partyId);
        ArgumentNullException.ThrowIfNull(targetScheme);

        return converter.ConvertOrDefaultAsync(partyId, RtSchemes.PartyId, targetScheme, ct);
    }

    public static ValueTask<string?> ConvertRtProductIdOrDefaultAsync(this ICodesConverter converter, string accountId, string targetScheme)
    {
        ArgumentNullException.ThrowIfNull(converter);
        ArgumentNullException.ThrowIfNull(accountId);
        ArgumentNullException.ThrowIfNull(targetScheme);

        return converter.ConvertOrDefaultAsync(accountId, RtSchemes.ProductId, targetScheme);
    }
}