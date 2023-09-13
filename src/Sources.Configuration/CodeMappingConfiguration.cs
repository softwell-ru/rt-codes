using System.ComponentModel.DataAnnotations;

namespace SoftWell.RtCodes.Sources.Configuration;

public class CodeMappingConfiguration
{
    [Required(AllowEmptyStrings = false)]
    public string Scheme { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Code { get; set; } = null!;
}
