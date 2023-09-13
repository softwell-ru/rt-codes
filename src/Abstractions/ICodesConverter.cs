namespace SoftWell.RtCodes;

public interface ICodesConverter
{
    ValueTask<string?> ConvertOrDefaultAsync(string code, string sourceScheme, string targetScheme, CancellationToken ct = default);
}
