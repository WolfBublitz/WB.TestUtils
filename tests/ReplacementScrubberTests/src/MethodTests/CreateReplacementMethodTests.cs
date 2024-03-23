namespace ReplacementScrubberTests.MethodTests.CreateReplacementMethodTests;

[TestClass]
public class TheCreateReplacementMethod
{
    [DataTestMethod]
    [DataRow(null, 0, "")]
    [DataRow(null, 1, " ")]
    [DataRow(null, 10, "          ")]
    [DataRow("test", 0, "")]
    [DataRow("test", 1, "{")]
    [DataRow("test", 2, "{}")]
    [DataRow("test", 3, "{t}")]
    [DataRow("test", 6, "{test}")]
    [DataRow("test", 10, "{test----}")]
    public void ShallCreateAReplacementString(string input, int length, string expectedReplacement)
    {
        // arrange / act
        string replacement = ReplacementScrubber.CreateReplacement(input, length);

        // assert
        replacement.Should().Be(expectedReplacement);
    }
}
