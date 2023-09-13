using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace SoftWell.RtCodes.Sources.Csv;

public class CsvCodesStreamReader : ICodesStreamReader
{
    public static readonly CsvCodesStreamReader Utf8Instance = new(Encoding.UTF8);

    private readonly IReaderConfiguration _config;

    public CsvCodesStreamReader(Encoding encoding)
    {
        _config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";",
            AllowComments = true,
            Comment = '#',
            Encoding = encoding
        };
    }

    public async IAsyncEnumerable<Code> ReadCodesAsync(
        Stream stream,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using var reader = new StreamReader(stream, encoding: _config.Encoding, leaveOpen: true);
        using var csv = new CsvReader(reader, _config, true);
        await foreach (var c in csv.GetRecordsAsync<Code>(ct))
        {
            yield return c;
        }
    }
}