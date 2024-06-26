using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WB.TestUtils;

/// <summary>
/// A data source attribute for MSTest that provides test data from the properties
/// of the decorated class.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
#pragma warning disable CA1813 // Nicht versiegelte Attribute vermeiden
public class TestDataObjectAttribute : Attribute, ITestDataSource
#pragma warning restore CA1813 // Nicht versiegelte Attribute vermeiden
{
    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                              │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the display name for the test data.
    /// </summary>
    /// <remarks>
    /// If this property is not set, the display name will be the name of the test method
    /// followed by the values of the test data parameters.
    /// </remarks>
#pragma warning disable CA1721 // Eigenschaftennamen dürfen nicht mit Get-Methoden übereinstimmen
    public string? DisplayName { get; init; }
#pragma warning restore CA1721 // Eigenschaftennamen dürfen nicht mit Get-Methoden übereinstimmen

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <inheritdoc/>
    public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        ArgumentNullException.ThrowIfNull(methodInfo, nameof(methodInfo));

        ParameterInfo[] parameterInfos = methodInfo.GetParameters();

        PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        object?[] parameters = new object[parameterInfos.Length];

        for (int i = 0; i < parameterInfos.Length; i++)
        {
            PropertyInfo? propertyInfo = propertyInfos.FirstOrDefault(p => string.Equals(p.Name, parameterInfos[i].Name, StringComparison.OrdinalIgnoreCase));

            if (propertyInfo is not null)
            {
                parameters[i] = propertyInfo.GetValue(this);
            }
            else
            {
                parameters[i] = null;
            }
        }

        yield return parameters;
    }

    /// <inheritdoc/>
    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        ArgumentNullException.ThrowIfNull(methodInfo, nameof(methodInfo));

        if (DisplayName is not null)
        {
            return DisplayName;
        }
        else
        {
            if (data is null)
            {
                return methodInfo.Name;
            }
            else
            {
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                string[] parameters = new string[parameterInfos.Length];
                int max = Math.Min(parameterInfos.Length, parameters.Length);

                for (int i = 0; i < max; i++)
                {
                    parameters[i] = data[i]?.ToString() ?? "null";
                }

                return $"{methodInfo.Name}({string.Join(", ", parameters)})";
            }
        }
    }
}

