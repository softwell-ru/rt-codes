namespace SoftWell.RtCodes.Sources;

public class HttpCodesSource : StreamCodesSource
{
    private readonly Func<HttpClient> _httpClientFactory;

    private readonly Uri _uri;

    public HttpCodesSource(
        Func<HttpClient> httpClientFactory,
        Uri uri,
        ICodesStreamReader streamReader) : base(streamReader)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _uri = uri ?? throw new ArgumentNullException(nameof(uri));
    }

    public override string Name => $"Http '{_uri}'";

    protected override async Task<Stream> GetStreamAsync(CancellationToken ct = default)
    {
        var client = _httpClientFactory();

        var response = await client.GetAsync(_uri, HttpCompletionOption.ResponseHeadersRead, ct);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(ct);

        return new DependantStream(stream, response, client);
    }

    private class DependantStream : Stream
    {
        private readonly Stream _inner;

        private readonly IDisposable[] _dependencies;

        public DependantStream(Stream inner, params IDisposable[] dependencies)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _dependencies = dependencies ?? Array.Empty<IDisposable>();
        }

        public override bool CanRead => _inner.CanRead;

        public override bool CanSeek => _inner.CanSeek;

        public override bool CanWrite => _inner.CanWrite;

        public override long Length => _inner.Length;

        public override long Position { get => _inner.Position; set => _inner.Position = value; }

        public override void Flush()
        {
            _inner.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _inner.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _inner.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _inner.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _inner.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _inner.Dispose();

                foreach (var d in _dependencies)
                {
                    d.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
