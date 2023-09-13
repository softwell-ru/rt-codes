namespace SoftWell.RtCodes;

/// <summary>
/// Источник кодов
/// </summary>
public interface ICodesSource
{
    /// <summary>
    /// Имя для логов
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Получение всех кодов из источника
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    IAsyncEnumerable<Code> GetCodesAsync(CancellationToken ct = default);
}
