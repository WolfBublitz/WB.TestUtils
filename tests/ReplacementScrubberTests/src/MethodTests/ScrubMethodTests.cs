using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ReplacementScrubberTests.MethodTests.ScrubMethodTests;

public class TheScrubMethod
{
    [TestClass]
    public class CalledWithoutReplacement
    {
        [DataTestMethod]
        [DynamicData(nameof(ShallReplaceAllMatchesByRegexData), DynamicDataSourceType.Method)]
        public void ShallReplaceAllMatchesByRegex(string input, Regex regex, bool replaceWithEqualLength, string expectedOutput)
        {
            // arrange / act
            string output = input.Scrub(regex, "replace", replaceWithEqualLength: replaceWithEqualLength);

            // assert
            output.Should().Be(expectedOutput);
        }

        private static IEnumerable<object[]> ShallReplaceAllMatchesByRegexData()
        {
            yield return new object[]
            {
                "test123",
                new Regex(@"(?'replace'(test))"),
                true,
                "    123"
            };

            yield return new object[]
            {
                "test123",
                new Regex(@"(?'replace'(test))"),
                false,
                "123"
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(ShallReplaceAllMatchesByStringData), DynamicDataSourceType.Method)]
        public void ShallReplaceAllMatchesByString(string input, string pattern, bool replaceWithEqualLength, string expectedOutput)
        {
            // arrange / act
            string output = input.Scrub(pattern, replaceWithEqualLength: replaceWithEqualLength);

            // assert
            output.Should().Be(expectedOutput);
        }

        private static IEnumerable<object[]> ShallReplaceAllMatchesByStringData()
        {
            yield return new object[]
            {
                "test123",
                "test",
                true,
                "    123"
            };

            yield return new object[]
            {
                "test123",
                "test",
                false,
                "123"
            };
        }
    }

    [TestClass]
    public class CalledWithReplacement
    {

        [DataTestMethod]
        [DynamicData(nameof(ShallReplaceAllMatchesByRegexData), DynamicDataSourceType.Method)]
        public void ShallReplaceAllMatchesByRegex(string input, Regex regex, string replacement, string expectedOutput)
        {
            // arrange / act
            string output = input.Scrub(regex, replacement, "replace");

            // assert
            output.Should().Be(expectedOutput);
        }

        private static IEnumerable<object[]> ShallReplaceAllMatchesByRegexData()
        {
            yield return new object[]
            {
            "test",
            new Regex(@"(?'replace'(test))"),
            null!,
            "    "
            };

            yield return new object[]
            {
            "test",
            new Regex(@"(?'replace'(test))"),
            "replacement",
            "{re}"
            };

            yield return new object[]
            {
            "This is a text for testing!",
            new Regex(@"(?'replace'(text))"),
            "a",
            "This is a {a-} for testing!"
            };

            yield return new object[]
            {
            "Test1 Test2 Test1 Test2 Test1 Test2",
            new Regex(@"(?'replace'(Test2))"),
            "T3",
            "Test1 {T3-} Test1 {T3-} Test1 {T3-}"
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(ShallReplaceAllMatchesByStringData), DynamicDataSourceType.Method)]
        public void ShallReplaceAllMatchesByString(string input, string pattern, string replacement, string expectedOutput)
        {
            // arrange / act
            string output = input.Scrub(pattern, replacement);

            // assert
            output.Should().Be(expectedOutput);
        }

        private static IEnumerable<object[]> ShallReplaceAllMatchesByStringData()
        {
            yield return new object[]
            {
            "test",
            "test",
            "replacement",
            "{re}"
            };

            yield return new object[]
            {
            "test",
            "test",
            null!,
            "    "
            };

            yield return new object[]
            {
            "This is a text for testing!",
            "text",
            "a",
            "This is a {a-} for testing!"
            };

            yield return new object[]
            {
            "Test1 Test2 Test1 Test2 Test1 Test2",
            "Test2",
            "T3",
            "Test1 {T3-} Test1 {T3-} Test1 {T3-}"
            };
        }
    }
}
