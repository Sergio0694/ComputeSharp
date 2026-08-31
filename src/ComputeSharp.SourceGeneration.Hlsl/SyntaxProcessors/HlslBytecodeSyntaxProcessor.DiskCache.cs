// RS1035 bans file IO in analyzers as a general policy, as analyzers are expected to be pure
// functions over compilation state. The persistent bytecode cache below is a deliberate,
// opt-in exception: it only runs when a project explicitly sets the MSBuild property enabling
// it, all IO failures are silently treated as cache misses (so it can never fail a build),
// and entries are content-addressed (so results never depend on external file state, and the
// output is guaranteed to be identical whether an entry is loaded from disk or recompiled).
#pragma warning disable RS1035

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using ComputeSharp.SourceGeneration.Models;
#if D2D1_SOURCE_GENERATOR
using HlslBytecodeInfoKey = ComputeSharp.D2D1.SourceGenerators.Models.HlslBytecodeInfoKey;
#else
using HlslBytecodeInfoKey = ComputeSharp.SourceGenerators.Models.HlslBytecodeInfoKey;
#endif

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <inheritdoc/>
partial class HlslBytecodeSyntaxProcessor
{
    /// <summary>
    /// The magic header for persistent cache entries (ie. "CSBC", for "ComputeSharp bytecode").
    /// </summary>
    private static ReadOnlySpan<byte> DiskCacheMagicHeader => "CSBC"u8;

    /// <summary>
    /// The format version for persistent cache entries. This must be incremented whenever the
    /// format of the serialized entries (or the meaning of any of their fields) is changed.
    /// </summary>
    private const byte DiskCacheFormatVersion = 1;

    /// <summary>
    /// The size of the header for persistent cache entries (magic header, format version, double precision flag, bytecode length).
    /// </summary>
    private const int DiskCacheHeaderSize = 4 + 1 + 1 + 4;

    /// <summary>
    /// Tries to load previously compiled bytecode for a given key from the persistent cache, if enabled.
    /// </summary>
    /// <param name="cacheDirectory">The directory for the persistent cache, if enabled.</param>
    /// <param name="key">The <see cref="HlslBytecodeInfoKey"/> instance for the shader to load.</param>
    /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
    /// <param name="info">The resulting <see cref="HlslBytecodeInfo"/> instance, if a valid cache entry was present.</param>
    /// <returns>Whether a valid cache entry was found (invalid entries are deleted, so they can be rewritten).</returns>
    private static bool TryReadDiskCache(
        string? cacheDirectory,
        HlslBytecodeInfoKey key,
        CancellationToken token,
        [NotNullWhen(true)] out HlslBytecodeInfo? info)
    {
        info = null;

        if (cacheDirectory is null)
        {
            return false;
        }

        try
        {
            string entryPath = GetDiskCacheEntryPath(cacheDirectory, key);
            byte[] data;

            try
            {
                data = File.ReadAllBytes(entryPath);
            }
            catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
            {
                return false;
            }

            token.ThrowIfCancellationRequested();

            // Validate the header and the payload length. An invalid entry (eg. from a truncated
            // write after a crash, or another version of the format) is deleted, so that the fresh
            // compilation following this cache miss can write a new valid entry in its place.
            if (data.Length <= DiskCacheHeaderSize ||
                !data.AsSpan(0, 4).SequenceEqual(DiskCacheMagicHeader) ||
                data[4] != DiskCacheFormatVersion ||
                data[5] is not (0 or 1) ||
                (data[6] | (data[7] << 8) | (data[8] << 16) | (data[9] << 24)) != data.Length - DiskCacheHeaderSize)
            {
                TryDeleteFile(entryPath);

                return false;
            }

            bool requiresDoublePrecisionSupport = data[5] == 1;
            byte[] array = new byte[data.Length - DiskCacheHeaderSize];

            Buffer.BlockCopy(data, DiskCacheHeaderSize, array, 0, array.Length);

            ImmutableArray<byte> bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);

            info = new HlslBytecodeInfo.Success(bytecode, requiresDoublePrecisionSupport);

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch
        {
            // Never let cache failures of any kind (eg. IO errors) fail the build: on
            // any error, just report a cache miss and fall back to a fresh compilation.
            return false;
        }
    }

    /// <summary>
    /// Tries to persist successfully compiled bytecode for a given key to the persistent cache, if enabled.
    /// </summary>
    /// <param name="cacheDirectory">The directory for the persistent cache, if enabled.</param>
    /// <param name="key">The <see cref="HlslBytecodeInfoKey"/> instance for the compiled shader.</param>
    /// <param name="info">The <see cref="HlslBytecodeInfo.Success"/> instance with the compiled bytecode.</param>
    private static void TryWriteDiskCache(string? cacheDirectory, HlslBytecodeInfoKey key, HlslBytecodeInfo.Success info)
    {
        if (cacheDirectory is null)
        {
            return;
        }

        try
        {
            _ = Directory.CreateDirectory(cacheDirectory);

            string entryPath = GetDiskCacheEntryPath(cacheDirectory, key);

            // If an entry is already present, there is nothing to do: entries are content-addressed,
            // so an existing entry is guaranteed to have the same contents (eg. another project that
            // is sharing the same cache directory may have compiled the same shader concurrently).
            if (File.Exists(entryPath))
            {
                return;
            }

            ImmutableArray<byte> bytecode = info.Bytecode.AsImmutableArray();
            byte[] array = Unsafe.As<ImmutableArray<byte>, byte[]>(ref bytecode);
            byte[] header = new byte[DiskCacheHeaderSize];

            DiskCacheMagicHeader.CopyTo(header);

            header[4] = DiskCacheFormatVersion;
            header[5] = info.RequiresDoublePrecisionSupport ? (byte)1 : (byte)0;
            header[6] = (byte)array.Length;
            header[7] = (byte)(array.Length >> 8);
            header[8] = (byte)(array.Length >> 16);
            header[9] = (byte)(array.Length >> 24);

            // Write to a temporary file first, and then move it into place. This guarantees that
            // concurrent readers can never observe a partially written entry (the move is atomic).
            string temporaryPath = $"{entryPath}.{Guid.NewGuid():N}.tmp";

            using (FileStream stream = File.Create(temporaryPath))
            {
                stream.Write(header, 0, header.Length);
                stream.Write(array, 0, array.Length);
            }

            try
            {
                File.Move(temporaryPath, entryPath);
            }
            catch
            {
                // Another process won the race to publish this entry (which necessarily has
                // the same contents), so just clean up the temporary file and move on.
                TryDeleteFile(temporaryPath);
            }
        }
        catch
        {
            // Ignore all errors: failing to persist an entry just means the bytecode will be compiled again
        }
    }

    /// <summary>
    /// Gets the path of the persistent cache entry for a given key.
    /// </summary>
    /// <param name="cacheDirectory">The directory for the persistent cache.</param>
    /// <param name="key">The <see cref="HlslBytecodeInfoKey"/> instance for the shader.</param>
    /// <returns>The path of the persistent cache entry for <paramref name="key"/>.</returns>
    /// <remarks>
    /// Entries are content-addressed: the file name is a hash over the compiler discriminator and compile
    /// parameters (see <see cref="AppendDiskCacheKey"/>), the generator version (conservatively invalidating
    /// all entries whenever the generator is updated), and the HLSL source. As such, a matching entry is
    /// guaranteed to hold exactly the bytecode that compiling the shader for this key would produce.
    /// </remarks>
    private static string GetDiskCacheEntryPath(string cacheDirectory, HlslBytecodeInfoKey key)
    {
        using IncrementalHash hash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);

        AppendDiskCacheKey(hash, key);

        hash.AppendData(Encoding.UTF8.GetBytes(typeof(HlslBytecodeSyntaxProcessor).Assembly.GetName().Version!.ToString()));
        hash.AppendData(Encoding.UTF8.GetBytes(key.HlslSource));

        byte[] hashBytes = hash.GetHashAndReset();

        StringBuilder fileName = new((hashBytes.Length * 2) + 4);

        foreach (byte b in hashBytes)
        {
            _ = fileName.Append(b.ToString("x2"));
        }

        _ = fileName.Append(".bin");

        return Path.Combine(cacheDirectory, fileName.ToString());
    }

    /// <summary>
    /// Tries to delete a given file, ignoring all errors.
    /// </summary>
    /// <param name="path">The path of the file to delete.</param>
    private static void TryDeleteFile(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch
        {
            // Ignore all errors (eg. the file being in use by a concurrent reader)
        }
    }

    /// <summary>
    /// Appends the compiler discriminator and the compile parameters for a given key to a persistent cache key hash.
    /// The HLSL source (and the generator version) are appended by the (shared) caller, so implementations only need
    /// to guarantee that keys producing different bytecode for the same HLSL source hash differently.
    /// </summary>
    /// <param name="hash">The <see cref="IncrementalHash"/> instance to append the data to.</param>
    /// <param name="key">The <see cref="HlslBytecodeInfoKey"/> instance for the shader.</param>
    private static partial void AppendDiskCacheKey(IncrementalHash hash, HlslBytecodeInfoKey key);
}
