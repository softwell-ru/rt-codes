using System.Runtime.CompilerServices;
using System.Text;

namespace SoftWell.RtCodes.Sources;

public static class CodesStreamReaderExtensions
{
    public static IAsyncEnumerable<Code> ReadCodesFromUtf8StringAsync(
        this ICodesStreamReader reader,
        string utf8String,
        CancellationToken ct = default)
    {
        return reader.ReadCodesFromStringAsync(utf8String, Encoding.UTF8, ct);
    }

    public static async IAsyncEnumerable<Code> ReadCodesFromStringAsync(
        this ICodesStreamReader reader,
        string str,
        Encoding encoding,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(reader);
        ArgumentNullException.ThrowIfNull(str);
        ArgumentNullException.ThrowIfNull(encoding);

        await using var stream = new MemoryStream(encoding.GetBytes(str));

        await foreach (var c in reader.ReadCodesAsync(stream, ct))
        {
            yield return c;
        }
    }
}