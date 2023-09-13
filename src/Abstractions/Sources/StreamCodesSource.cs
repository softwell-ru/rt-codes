using System.Runtime.CompilerServices;

namespace SoftWell.RtCodes.Sources;

public abstract class StreamCodesSource : ICodesSource
{
    private readonly ICodesStreamReader _streamReader;

    protected StreamCodesSource(ICodesStreamReader streamReader)
    {
        _streamReader = streamReader ?? throw new ArgumentNullException(nameof(streamReader));
    }

    public abstract string Name { get; }

    public async IAsyncEnumerable<Code> GetCodesAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await using var stream = await GetStreamAsync(ct);

        await foreach (var c in _streamReader.ReadCodesAsync(stream, ct))
        {
            yield return c;
        }
    }

    protected abstract Task<Stream> GetStreamAsync(CancellationToken ct = default);
}
