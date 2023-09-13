using SoftWell.RtCodes.Sources.Tests.Base;

namespace SoftWell.RtCodes.Sources.Csv.Tests;

[TestClass]
public class DeserializationTests
{
    [TestMethod]
    public async Task Should_ReadCodes_FromCsv()
    {
        var csv = """
SourceScheme;SourceCodeValue;TargetScheme;TargetCodeValue
#scheme1
scheme1;code1;target-scheme1;123
#scheme2
scheme2;code2;target-scheme2;456
scheme2;code2;target-scheme3;789
scheme2;code3;target-scheme3;012
""";

        var codes = await CsvCodesStreamReader.Utf8Instance.ReadCodesFromUtf8StringAsync(csv).ToListAsync();

        TestHelpers.AssertTestCodes(codes);
    }
}