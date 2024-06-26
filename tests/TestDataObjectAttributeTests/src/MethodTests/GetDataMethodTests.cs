using System.Linq;
using System.Reflection;
using WB.TestUtils;

namespace TestDataAttributeTests.MethodTests.GetDataMethodTests;

public class TestDataAttribute : TestDataObjectAttribute
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
    public void TestMethodWithTestDataObject(TestDataAttribute _)
    {
    }

    public void TestMethod(int intValue, string stringValue, object objectValue)
    {
    }

    [TestMethod]
    public void ShouldReturnTheTestDataObject()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDataMethod).GetMethod(nameof(TestMethodWithTestDataObject))!;
        TestDataAttribute testData = new();

        // Act
        object?[] data = testData.GetData(methodInfo).First();

        // Assert
        data.Should().BeEquivalentTo(new object?[] { testData });
    }

    [TestMethod]
    public void ShouldReturnAnArrayWithThePropertyValues()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDataMethod).GetMethod(nameof(TestMethod))!;
        TestDataAttribute testData = new();

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
