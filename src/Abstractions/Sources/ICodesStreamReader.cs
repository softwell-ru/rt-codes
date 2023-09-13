namespace SoftWell.RtCodes.Sources;

public interface ICodesStreamReader
{
    IAsyncEnumerable<Code> ReadCodesAsync(
        Stream stream,
        CancellationToken ct = default);
}
