namespace SoftWell.RtCodes;

/// <summary>
/// Маппинг кода
/// </summary>
public record Code
{
    /// <summary>
    /// Исходныя схема
    /// </summary>
    public required string SourceScheme { get; init; }

    /// <summary>
    /// Значение кода в исходной схеме
    /// </summary>
    public required string SourceCodeValue { get; init; }

    /// <summary>
    /// Целевая схема
    /// </summary>
    public required string TargetScheme { get; init; }

    /// <summary>
    /// Значение кода в целевой схеме
    /// </summary>
    public required string TargetCodeValue { get; init; }
}