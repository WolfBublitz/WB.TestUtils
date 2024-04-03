using System.Threading.Tasks;
using System.IO;

namespace DateTimeScrubberTests.MethodTests.ScrubDateTimeMethodTests;

[TestClass]
public class TheScrubVersionNumbersMethod : VerifyBase
{
    private static readonly string VerificationFilePath = Path.Combine("Verification");

    [DataTestMethod]
    [DataRow("05.04.2063 00:00:00",
             "dd.MM.yyyy HH:mm:ss",
             "                   ")]
    [DataRow("05.04.2063 00:00:00.0000000",
             "dd.MM.yyyy HH:mm:ss.fffffff",
             "                           ")]
    public void ShallReplaceADateTime(string input, string format, string expectedOutput)
    {
        // arrange / act
        string output = input.ScrubDateTime(format);

        // assert
        output.Should().Be(expectedOutput);
    }

    [DataTestMethod]
    [DataRow("05.04.2063 00:00:00",
             "dd.MM.yyyy HH:mm:ss",
             "{Something--------}")]
    [DataRow("05.04.2063 00:00:00.0000000",
             "dd.MM.yyyy HH:mm:ss.fffffff",
             "{Something----------------}")]
    public void ShallReplaceADateTimeWithACustomReplacement(string input, string format, string expectedOutput)
    {
        // arrange / act
        string output = input.ScrubDateTime(format, replacement: "Something");

        // assert
        output.Should().Be(expectedOutput);
    }
}
