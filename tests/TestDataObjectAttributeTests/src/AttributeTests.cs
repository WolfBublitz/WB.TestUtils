using WB.TestUtils;

namespace TestDataAttributeTests.AttributeTests;

public class TestData : TestDataObjectAttribute
{
    public int IntValue { get; set; }

    public string? StringValue { get; set; }

    public object? ObjectValue { get; set; }
}

[TestClass]
public class TheTestDataAttribute
{
    [DataTestMethod]
    [TestData(IntValue = 42, StringValue = "Hello World!", ObjectValue = null)]
    public void CanBeInjectedDirectory(TestData testData)
    {
        // Assert
        testData.IntValue.Should().Be(42);
        testData.StringValue.Should().Be("Hello World!");
        testData.ObjectValue.Should().BeNull();
    }

    [DataTestMethod]
    [TestData(IntValue = 42, StringValue = "Hello World!", ObjectValue = null)]
    public void ShouldProvideEachPropertyAsTestParameter(int intValue, string stringValue, object objectValue)
    {
        // Assert
        intValue.Should().Be(42);
        stringValue.Should().Be("Hello World!");
        objectValue.Should().BeNull();
    }
}
