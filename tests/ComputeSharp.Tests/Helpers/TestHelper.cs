using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Helpers;

/// <summary>
/// A helper class for test methods.
/// </summary>
public static class TestHelper
{
    /// <summary>
    /// Runs a generic test methods with the specified arguments.
    /// </summary>
    /// <param name="delegate">A <see cref="Delegate"/> instance wrapping the target method.</param>
    /// <param name="type">The type argument to use to invoke <paramref name="delegate"/>.</param>
    /// <param name="arguments">The arguments to use to invoke <paramref name="delegate"/>.</param>
    public static void Run(Delegate @delegate, Type type, params object[] arguments)
    {
        if (@delegate.Method is not { IsStatic: true, IsGenericMethod: true })
        {
            Assert.Fail("The input delegate must point to a generic static method");
        }

        try
        {
            @delegate.Method.GetGenericMethodDefinition().MakeGenericMethod(type).Invoke(null, arguments);
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            throw e.InnerException;
        }
    }

    /// <summary>
    /// Runs a generic test methods with the specified arguments.
    /// </summary>
    /// <param name="delegate">A <see cref="Delegate"/> instance wrapping the target method.</param>
    /// <param name="type1">The first type argument to use to invoke <paramref name="delegate"/>.</param>
    /// <param name="type2">The second type argument to use to invoke <paramref name="delegate"/>.</param>
    /// <param name="arguments">The arguments to use to invoke <paramref name="delegate"/>.</param>
    public static void Run(Delegate @delegate, Type type1, Type type2, params object[] arguments)
    {
        if (@delegate.Method is not { IsStatic: true, IsGenericMethod: true })
        {
            Assert.Fail("The input delegate must point to a generic static method");
        }

        try
        {
            @delegate.Method.GetGenericMethodDefinition().MakeGenericMethod(type1, type2).Invoke(null, arguments);
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            throw e.InnerException;
        }
    }
}
