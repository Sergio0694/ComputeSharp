using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// A custom <see cref="TestMethodAttribute"/> implementation that also acts as a combinatorial <see cref="ITestDataSource"/>
/// instance. It has dedicated paths for target devices and resource types, and arbitrary inputs for the test methods.
/// </summary>
/// <remarks>
/// Use this attribute in combination with <see cref="DeviceAttribute"/>, <see cref="AllDevicesAttribute"/>,
/// <see cref="ResourceAttribute"/> or <see cref="DataAttribute"/>, not other existing MSTest attributes.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class CombinatorialTestMethodAttribute : TestMethodAttribute, ITestDataSource
{
    /// <inheritdoc/>
    IEnumerable<object?[]> ITestDataSource.GetData(MethodInfo methodInfo)
    {
        Device[] devices;

        if (methodInfo.GetCustomAttribute<AllDevicesAttribute>() is not null)
        {
            devices = (Device[])Enum.GetValues(typeof(Device));
        }
        else if (methodInfo.GetCustomAttributes<DeviceAttribute>().ToArray() is { Length: > 0 } values)
        {
            devices = [.. values.Select(static value => value.Device)];
        }
        else
        {
            devices = [];
        }

        Type[] resources = [.. methodInfo.GetCustomAttributes<ResourceAttribute>().Select(static value => value.Type)];
        Type[] additionalResources = [.. methodInfo.GetCustomAttributes<AdditionalResourceAttribute>().Select(static value => value.Type)];
        object[][] data = [.. methodInfo.GetCustomAttributes<DataAttribute>().Select(static value => value.Data)];
        object[][] additionalData = [.. methodInfo.GetCustomAttributes<AdditionalDataAttribute>().Select(static value => value.Data)];

        if (devices.Length > 0)
        {
            foreach (Device device in devices)
            {
                if (resources.Length > 0)
                {
                    foreach (Type type in resources)
                    {
                        if (additionalResources.Length > 0)
                        {
                            foreach (Type additionalType in additionalResources)
                            {
                                if (data.Length == 0)
                                {
                                    if (additionalData.Length > 0)
                                    {
                                        Assert.Fail("Invalid usage of [AdditionalData]");
                                    }

                                    yield return [device, type, additionalType];
                                }
                                else
                                {
                                    if (additionalData.Length > 0)
                                    {
                                        foreach (object[] items in data)
                                        {
                                            foreach (object[] additionalItems in additionalData)
                                            {
                                                yield return [device, type, additionalType, .. items, .. additionalItems];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (object[] items in data)
                                        {
                                            yield return [device, type, additionalType, .. items];
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (data.Length == 0)
                            {
                                if (additionalData.Length > 0)
                                {
                                    Assert.Fail("Invalid usage of [AdditionalData]");
                                }

                                yield return [device, type];
                            }
                            else
                            {
                                if (additionalData.Length > 0)
                                {
                                    foreach (object[] items in data)
                                    {
                                        foreach (object[] additionalItems in additionalData)
                                        {
                                            yield return [device, type, .. items, .. additionalItems];
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (object[] items in data)
                                    {
                                        yield return [device, type, .. items];
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (additionalResources.Length > 0)
                    {
                        Assert.Fail("Invalid usage of [AdditionalResource]");
                    }

                    if (data.Length == 0)
                    {
                        if (additionalData.Length > 0)
                        {
                            Assert.Fail("Invalid usage of [AdditionalData]");
                        }

                        yield return [device];
                    }
                    else
                    {
                        if (additionalData.Length > 0)
                        {
                            foreach (object[] items in data)
                            {
                                foreach (object[] additionalItems in additionalData)
                                {
                                    yield return [device, .. items, .. additionalItems];
                                }
                            }
                        }
                        else
                        {
                            foreach (object[] items in data)
                            {
                                yield return [device, .. items];
                            }
                        }
                    }
                }
            }
        }
        else if (resources.Length > 0)
        {
            foreach (Type type in resources)
            {
                if (additionalResources.Length > 0)
                {
                    foreach (Type additionalType in additionalResources)
                    {
                        if (data.Length == 0)
                        {
                            if (additionalData.Length > 0)
                            {
                                Assert.Fail("Invalid usage of [AdditionalData]");
                            }

                            yield return [type, additionalType];
                        }
                        else
                        {
                            if (additionalData.Length > 0)
                            {
                                foreach (object[] items in data)
                                {
                                    foreach (object[] additionalItems in additionalData)
                                    {
                                        yield return [type, additionalType, .. items, .. additionalItems];
                                    }
                                }
                            }
                            else
                            {
                                foreach (object[] items in data)
                                {
                                    yield return [type, additionalType, .. items];
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (data.Length == 0)
                    {
                        if (additionalData.Length > 0)
                        {
                            Assert.Fail("Invalid usage of [AdditionalData]");
                        }

                        yield return [type];
                    }
                    else
                    {
                        if (additionalData.Length > 0)
                        {
                            foreach (object[] items in data)
                            {
                                foreach (object[] additionalItems in additionalData)
                                {
                                    yield return [type, .. items, .. additionalItems];
                                }
                            }
                        }
                        else
                        {
                            foreach (object[] items in data)
                            {
                                yield return [type, .. items];
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (additionalResources.Length > 0)
            {
                Assert.Fail("Invalid usage of [AdditionalResource]");
            }

            if (data.Length == 0)
            {
                if (additionalData.Length > 0)
                {
                    Assert.Fail("Invalid usage of [AdditionalData]");
                }

                yield return [];
            }
            else
            {
                if (additionalData.Length > 0)
                {
                    foreach (object[] items in data)
                    {
                        foreach (object[] additionalItems in additionalData)
                        {
                            yield return [.. items, .. additionalItems];
                        }
                    }
                }
                else
                {
                    foreach (object[] items in data)
                    {
                        yield return items;
                    }
                }
            }
        }
    }

    /// <inheritdoc/>
    string? ITestDataSource.GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        return $"{methodInfo.Name} ({string.Join(", ", data ?? [])})";
    }
}