using System.ComponentModel.DataAnnotations;

namespace SoftWell.RtCodes.Sources.Configuration;

public class CodesInSchemeMappingConfiguration
{
    [Required(AllowEmptyStrings = false)]
    public string Code { get; set; } = null!;

    [Required]
    public CodeMappingConfiguration[] Mapping { get; set; } = null!;
}
