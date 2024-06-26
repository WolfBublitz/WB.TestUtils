using System;
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

    [TestMethod]
    public void ShouldReturnTheMethodNameIfDataIsNull()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDisplayNameProperty).GetMethod(nameof(TestMethodWithoutParameter))!;
        TestDataObjectAttribute testDataObjectAttribute = new();

        // Act
        string? displayName = testDataObjectAttribute.GetDisplayName(methodInfo, null);

        // Assert
        displayName.Should().Be(methodInfo.Name + "()");
    }

    [TestMethod]
    public void ShouldReturnTheMethodNameIfDataIsEmpty()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDisplayNameProperty).GetMethod(nameof(TestMethodWithoutParameter))!;
        TestDataObjectAttribute testDataObjectAttribute = new();

        // Act
        string? displayName = testDataObjectAttribute.GetDisplayName(methodInfo, Array.Empty<object>());

        // Assert
        displayName.Should().Be(methodInfo.Name + "()");
    }

    [TestMethod]
    public void ShouldReturnTheMethodNameWithParameters()
    {
        // Arrange
        MethodInfo methodInfo = typeof(TheDisplayNameProperty).GetMethod(nameof(TestMethodWithoutParameter))!;
        TestDataObjectAttribute testDataObjectAttribute = new();

        // Act
        string? displayName = testDataObjectAttribute.GetDisplayName(methodInfo, [42, "Hello World!", true]);

        // Assert
        displayName.Should().Be(methodInfo.Name + "(42, Hello World!, True)");
    }
}
