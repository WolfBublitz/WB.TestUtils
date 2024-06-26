using WB.TestUtils;

namespace TestDataAttributeTests.AttributeTests;

public class TestData : TestDataObjectAttribute
{
    public int IntValue { get; set; } = 42;

    public string StringValue { get; set; } = "Hello, World!";

    public object ObjectValue { get; set; } = new object();
}

[TestClass]
public class TheTestDataAttribute
{
    [DataTestMethod]
    [TestData]
    public void CanBeInjectedDirectory(TestData testData)
    {
        // Assert
        testData.IntValue.Should().Be(42);
        testData.StringValue.Should().Be("Hello, World!");
        testData.ObjectValue.Should().NotBeNull();
    }

    [DataTestMethod]
    [TestData]
    public void ShouldProvideEachPropertyAsTestParameter(int intValue, string stringValue, object objectValue)
    {
        // Assert
        intValue.Should().Be(42);
        stringValue.Should().Be("Hello, World!");
        objectValue.Should().NotBeNull();
    }
}
