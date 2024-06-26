using System.Linq;
using System.Reflection;
using WB.TestUtils;

namespace TestDataAttributeTests.MethodTests.GetDataMethodTests;

public class CompleteTestDataAttribute : TestDataObjectAttribute
{
    public int IntValue { get; set; } = 42;

    public string StringValue { get; set; } = "Hello, World!";

    public object ObjectValue { get; set; } = new object();

}

public class EmptyTestDataAttribute : TestDataObjectAttribute
{
}

[TestClass]
public class TheDataMethod
{
    public void TestMethod(int intValue, string stringValue, object objectValue)
    {
    }

    [TestMethod]
    public void ShouldReturnTheValuesFromTheSameNamedProperty()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDataMethod).GetMethod(nameof(TestMethod))!;
        CompleteTestDataAttribute testData = new();

        // Act
        object?[] data = testData.GetData(methodInfo).First();

        // Assert
        data.Should().BeEquivalentTo(new object?[] { testData.IntValue, testData.StringValue, testData.ObjectValue });
    }

    [TestMethod]
    public void ShouldReturnNullForMissingProperties()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDataMethod).GetMethod(nameof(TestMethod))!;
        EmptyTestDataAttribute testData = new();

        // Act
        object?[] data = testData.GetData(methodInfo).First();

        // Assert
        data.Should().BeEquivalentTo(new object?[] { null, null, null });
    }
}
