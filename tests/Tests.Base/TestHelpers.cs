namespace SoftWell.RtCodes.Sources.Tests.Base;

public static class TestHelpers
{
    public static readonly IReadOnlyList<Code> TestCodes = new[]{
        new Code
        {
            SourceScheme = "scheme1",
            SourceCodeValue = "code1",
            TargetScheme = "target-scheme1",
            TargetCodeValue = "123"
        },
        new Code
        {
            SourceScheme = "scheme2",
            SourceCodeValue = "code2",
            TargetScheme = "target-scheme2",
            TargetCodeValue = "456"
        },
        new Code
        {
            SourceScheme = "scheme2",
            SourceCodeValue = "code2",
            TargetScheme = "target-scheme3",
            TargetCodeValue = "789"
        },
        new Code
        {
            SourceScheme = "scheme2",
            SourceCodeValue = "code3",
            TargetScheme = "target-scheme3",
            TargetCodeValue = "012"
        }
    };

    public static void AssertTestCodes(IEnumerable<Code> codes)
    {
        codes.Should().BeEquivalentTo(TestCodes);
    }
}