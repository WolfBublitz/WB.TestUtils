using WB.TestUtils;

namespace TestDataAttributeTests.AttributeTests;

internal class NotTestDataAttribute : TestDataObjectAttribute
{
}

internal class SomeTestDataAttribute : TestDataObjectAttribute
{
    public int IntValue { get; set; } = 42;

    public string StringValue { get; set; } = "Hello, World!";

    public object ObjectValue { get; set; } = new object();
}

[TestClass]
public class TheTestDataAttribute
{
    [DataTestMethod]
    [NotTestData]
    public void ShouldProvideDefaultValuesIfParametersAreNotSet(int intValue, string stringValue, object objectValue)
    {
        // Assert
        intValue.Should().Be(0);
        stringValue.Should().BeNull();
        objectValue.Should().BeNull();
    }

    [DataTestMethod]
    [SomeTestData]
    public void ShouldProvideTestParametersFromProperties(int intValue, string stringValue, object objectValue)
    {
        // Assert
        intValue.Should().Be(42);
        stringValue.Should().Be("Hello, World!");
        objectValue.Should().NotBeNull();
    }
}
