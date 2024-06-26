using System.Reflection;
using WB.TestUtils;

namespace TestDataAttributeTests.PropertyTests.DisplayNamePropertyTests;

[TestClass]
public class TheDisplayNameProperty
{
    public void TestMethodWithoutParameter()
    {
    }

    public void TestMethodWithParameters(int intValue, string stringValue, bool boolValue)
    {
    }

    [TestMethod]
    public void ShouldBeTheNameOfTheDisplayNameProperty()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDisplayNameProperty).GetMethod(nameof(TestMethodWithoutParameter))!;
        TestDataObjectAttribute testDataObjectAttribute = new()
        {
            DisplayName = "CustomDisplayName",
        };

        // Act
        string? displayName = testDataObjectAttribute.GetDisplayName(methodInfo, new object?[] { });

        // Assert
        displayName.Should().Be(testDataObjectAttribute.DisplayName);
    }

    [DataTestMethod]
    [DataRow(nameof(TestMethodWithoutParameter), "TestMethodWithoutParameter()")]
    [DataRow(nameof(TestMethodWithParameters), "TestMethodWithParameters(42, Hello World, False)")]
    public void ShouldBeTheNameOfTheTestMethodFollowedByTheValuesOfTheTestDataParameters(string methodName, string expectedDisplayName)
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDisplayNameProperty).GetMethod(methodName)!;
        TestDataObjectAttribute testData = new();

        // Act
        string? displayName = testData.GetDisplayName(methodInfo, [42, "Hello World", false]);

        // Assert
        displayName.Should().Be(expectedDisplayName);
    }
}
