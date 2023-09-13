using System.ComponentModel.DataAnnotations;

namespace SoftWell.RtCodes.Sources.Configuration;

public class CodesMappingConfiguration
{
    [Required]
    public SchemeMappingConfiguration[] Schemes { get; set; } = null!;
}
