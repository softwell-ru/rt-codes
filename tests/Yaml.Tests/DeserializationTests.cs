using SoftWell.RtCodes.Sources.Tests.Base;

namespace SoftWell.RtCodes.Sources.Yaml.Tests;

[TestClass]
public class DeserializationTests
{
    [TestMethod]
    public async Task Should_ReadCodes_FromYaml()
    {
        var yaml = """
scheme1:
  code1:
    target-scheme1: 123
scheme2:
  code2:
    target-scheme2: 456
    target-scheme3: 789
  code3:
    target-scheme3: 012
""";

        var codes = await YamlCodesStreamReader.Utf8Instance.ReadCodesFromUtf8StringAsync(yaml).ToListAsync();
        TestHelpers.AssertTestCodes(codes);
    }
}