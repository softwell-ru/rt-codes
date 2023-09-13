namespace SoftWell.RtCodes.Sources;

public class FileCodesSource : StreamCodesSource
{
    private readonly string _path;

    public FileCodesSource(string path, ICodesStreamReader streamReader) : base(streamReader)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public override string Name => $"File '{_path}'";

    protected override Task<Stream> GetStreamAsync(CancellationToken ct = default)
    {
        return Task.FromResult<Stream>(File.OpenRead(_path));
    }
}