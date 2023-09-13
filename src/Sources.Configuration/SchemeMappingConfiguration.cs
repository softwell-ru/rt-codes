using System.ComponentModel.DataAnnotations;

namespace SoftWell.RtCodes.Sources.Configuration;

public class SchemeMappingConfiguration
{
    [Required(AllowEmptyStrings = false)]
    public string Scheme { get; set; } = null!;

    [Required]
    public CodesInSchemeMappingConfiguration[] Codes { get; set; } = null!;
}
