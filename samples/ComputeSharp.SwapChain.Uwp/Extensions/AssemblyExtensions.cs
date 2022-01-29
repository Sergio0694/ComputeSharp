using System.IO;
using System.Reflection;
using CommunityToolkit.Diagnostics;

#nullable enable

namespace ComputeSharp.SwapChain.Uwp.Extensions;

/// <summary>
/// Helpers for the <see cref="Assembly"/> type.
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// Tries to read a <see cref="string"/> from a specified file in the target <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The target <see cref="Assembly"/> to read data from.</param>
    /// <param name="filename">The target filename to read from.</param>
    /// <param name="text">The resulting text, if the read was successful.</param>
    /// <returns>The contents of the specified file.</returns>
    public static bool TryReadAllTextFromManifestFile(this Assembly assembly, string filename, out string? text)
    {
        Guard.IsNotNull(assembly, nameof(assembly));
        Guard.IsNotNull(filename, nameof(filename));

        try
        {
            string manifestFilename = $"{assembly.GetName().Name}.{filename.Replace('/', '.').Replace('\\', '.')}";

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestFilename);
            using StreamReader reader = new(stream);

            text = reader.ReadToEnd().Trim();

            return true;
        }
        catch
        {
            text = null;

            return false;
        }
    }
}
