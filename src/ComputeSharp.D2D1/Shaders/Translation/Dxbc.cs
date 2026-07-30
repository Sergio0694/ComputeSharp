using System;
using System.Buffers.Binary;

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <summary>
/// A helper type to inspect and patch DXBC shader containers produced by FXC.
/// </summary>
/// <remarks>
/// <para>A DXBC container is a header followed by an unordered sequence of blobs:</para>
/// <list type="bullet">
///   <item>A 4 bytes <c>'DXBC'</c> signature.</item>
///   <item>A 16 bytes checksum of the rest of the container.</item>
///   <item>A 4 bytes version, a 4 bytes total container size, and a 4 bytes blob count.</item>
///   <item>One 4 bytes offset per blob, relative to the start of the container.</item>
///   <item>Each blob, made of a 4 bytes signature, a 4 bytes payload size, and the payload.</item>
/// </list>
/// <para>All values are stored in little endian order.</para>
/// </remarks>
internal static partial class Dxbc
{
    /// <summary>
    /// The <c>'DXBC'</c> signature at the start of every DXBC container.
    /// </summary>
    private const uint ContainerSignature = 0x43425844;

    /// <summary>
    /// The <c>'SFI0'</c> signature of the shader feature info blob.
    /// </summary>
    private const uint ShaderFeatureInfoSignature = 0x30494653;

    /// <summary>
    /// The shader feature flag indicating that a shader declares minimum precision support.
    /// </summary>
    /// <remarks>
    /// This matches <c>D3D_SHADER_FEATURE_MINIMUM_PRECISION</c> from <c>d3dcommon.h</c>.
    /// </remarks>
    private const ulong MinimumPrecisionShaderFeatureFlag = 0x0010;

    /// <summary>
    /// The offset of the container signature.
    /// </summary>
    private const int SignatureOffset = 0;

    /// <summary>
    /// The offset of the total container size.
    /// </summary>
    private const int ContainerSizeOffset = 24;

    /// <summary>
    /// The offset of the blob count.
    /// </summary>
    private const int BlobCountOffset = 28;

    /// <summary>
    /// The offset of the table of blob offsets, which is also the size of the fixed header.
    /// </summary>
    private const int BlobOffsetsOffset = 32;

    /// <summary>
    /// The size of the header of each blob (a signature and a payload size).
    /// </summary>
    private const int BlobHeaderSize = 8;

    /// <summary>
    /// The size of the payload of a shader feature info blob (a single 64 bit set of flags).
    /// </summary>
    private const int ShaderFeatureInfoPayloadSize = 8;

    /// <summary>
    /// Creates a copy of a DXBC container that declares minimum precision support.
    /// </summary>
    /// <param name="bytecode">The DXBC container to copy and patch.</param>
    /// <returns>A copy of <paramref name="bytecode"/> declaring minimum precision support.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="bytecode"/> is not a well formed DXBC container.</exception>
    /// <remarks>
    /// If the input container already has a shader feature info blob, the minimum precision flag is set on it.
    /// Otherwise, a new shader feature info blob declaring just that flag is appended to the container.
    /// </remarks>
    public static byte[] CreateWithMinimumPrecisionShaderFeatureFlag(ReadOnlySpan<byte> bytecode)
    {
        int blobCount = ValidateAndGetBlobCount(bytecode);
        byte[] patchedBytecode;

        // If the container already declares its shader features, just set the flag in place
        if (TryGetShaderFeatureInfoPayloadOffset(bytecode, blobCount, out int payloadOffset))
        {
            patchedBytecode = bytecode.ToArray();

            ulong featureFlags = BinaryPrimitives.ReadUInt64LittleEndian(patchedBytecode.AsSpan(payloadOffset));

            BinaryPrimitives.WriteUInt64LittleEndian(
                patchedBytecode.AsSpan(payloadOffset),
                featureFlags | MinimumPrecisionShaderFeatureFlag);
        }
        else
        {
            patchedBytecode = CreateWithShaderFeatureInfoBlob(bytecode, blobCount);
        }

        // The contents of the container changed, so its checksum has to be recomputed. FXC APIs
        // such as D3DSetBlobPart validate the checksum of their input and reject it otherwise.
        UpdateChecksum(patchedBytecode);

        return patchedBytecode;
    }

    /// <summary>
    /// Creates a copy of a DXBC container with an additional shader feature info blob appended to it.
    /// </summary>
    /// <param name="bytecode">The DXBC container to copy and patch.</param>
    /// <param name="blobCount">The number of blobs in <paramref name="bytecode"/>.</param>
    /// <returns>A copy of <paramref name="bytecode"/> with a shader feature info blob.</returns>
    private static byte[] CreateWithShaderFeatureInfoBlob(ReadOnlySpan<byte> bytecode, int blobCount)
    {
        // Appending a blob also adds one entry to the table of blob offsets, which shifts the
        // body of the container (ie. all existing blobs) forward by the size of that entry.
        const int BlobOffsetSize = sizeof(uint);
        const int AppendedBlobSize = BlobHeaderSize + ShaderFeatureInfoPayloadSize;

        int bodyOffset = BlobOffsetsOffset + (blobCount * BlobOffsetSize);
        int patchedBodyOffset = bodyOffset + BlobOffsetSize;
        int patchedBlobOffset = bytecode.Length + BlobOffsetSize;

        byte[] patchedBytecode = new byte[bytecode.Length + BlobOffsetSize + AppendedBlobSize];

        // Copy the fixed header, then the existing body after the enlarged table of blob offsets
        bytecode.Slice(0, BlobOffsetsOffset).CopyTo(patchedBytecode);
        bytecode.Slice(bodyOffset).CopyTo(patchedBytecode.AsSpan(patchedBodyOffset));

        BinaryPrimitives.WriteUInt32LittleEndian(patchedBytecode.AsSpan(ContainerSizeOffset), (uint)patchedBytecode.Length);
        BinaryPrimitives.WriteUInt32LittleEndian(patchedBytecode.AsSpan(BlobCountOffset), (uint)(blobCount + 1));

        // Shift the existing blob offsets to account for the new entry in the table
        for (int i = 0; i < blobCount; i++)
        {
            int blobOffsetOffset = BlobOffsetsOffset + (i * BlobOffsetSize);
            uint blobOffset = BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(blobOffsetOffset));

            BinaryPrimitives.WriteUInt32LittleEndian(patchedBytecode.AsSpan(blobOffsetOffset), blobOffset + BlobOffsetSize);
        }

        // Add the entry for the appended blob, and then the blob itself
        BinaryPrimitives.WriteUInt32LittleEndian(patchedBytecode.AsSpan(bodyOffset), (uint)patchedBlobOffset);
        BinaryPrimitives.WriteUInt32LittleEndian(patchedBytecode.AsSpan(patchedBlobOffset), ShaderFeatureInfoSignature);
        BinaryPrimitives.WriteUInt32LittleEndian(patchedBytecode.AsSpan(patchedBlobOffset + BlobOffsetSize), ShaderFeatureInfoPayloadSize);
        BinaryPrimitives.WriteUInt64LittleEndian(patchedBytecode.AsSpan(patchedBlobOffset + BlobHeaderSize), MinimumPrecisionShaderFeatureFlag);

        return patchedBytecode;
    }

    /// <summary>
    /// Validates that a given buffer is a well formed DXBC container, and gets the number of blobs in it.
    /// </summary>
    /// <param name="bytecode">The DXBC container to validate.</param>
    /// <returns>The number of blobs in <paramref name="bytecode"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="bytecode"/> is not a well formed DXBC container.</exception>
    private static int ValidateAndGetBlobCount(ReadOnlySpan<byte> bytecode)
    {
        if (bytecode.Length < BlobOffsetsOffset ||
            BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(SignatureOffset)) != ContainerSignature ||
            BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(ContainerSizeOffset)) != (uint)bytecode.Length)
        {
            return ThrowArgumentExceptionForInvalidContainer();
        }

        uint blobCount = BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(BlobCountOffset));

        // Ensure the table of blob offsets is in bounds before walking it
        if (BlobOffsetsOffset + ((long)blobCount * sizeof(uint)) > bytecode.Length)
        {
            return ThrowArgumentExceptionForInvalidContainer();
        }

        for (int i = 0; i < blobCount; i++)
        {
            uint blobOffset = BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(BlobOffsetsOffset + (i * sizeof(uint))));

            // Ensure the header of the blob is in bounds before reading the size of its payload from it
            if (blobOffset + (long)BlobHeaderSize > bytecode.Length)
            {
                return ThrowArgumentExceptionForInvalidContainer();
            }

            uint blobSize = BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice((int)blobOffset + sizeof(uint)));

            if (blobOffset + (long)BlobHeaderSize + blobSize > bytecode.Length)
            {
                return ThrowArgumentExceptionForInvalidContainer();
            }
        }

        return (int)blobCount;
    }

    /// <summary>
    /// Tries to get the offset of the payload of the shader feature info blob in a DXBC container.
    /// </summary>
    /// <param name="bytecode">The DXBC container to inspect.</param>
    /// <param name="blobCount">The number of blobs in <paramref name="bytecode"/>.</param>
    /// <param name="payloadOffset">The resulting offset of the shader feature info payload, if found.</param>
    /// <returns>Whether the shader feature info blob was found.</returns>
    private static bool TryGetShaderFeatureInfoPayloadOffset(ReadOnlySpan<byte> bytecode, int blobCount, out int payloadOffset)
    {
        for (int i = 0; i < blobCount; i++)
        {
            int blobOffset = (int)BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(BlobOffsetsOffset + (i * sizeof(uint))));

            // Only consider the blob if it can actually hold a full set of shader feature flags
            if (BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(blobOffset)) == ShaderFeatureInfoSignature &&
                BinaryPrimitives.ReadUInt32LittleEndian(bytecode.Slice(blobOffset + sizeof(uint))) >= ShaderFeatureInfoPayloadSize)
            {
                payloadOffset = blobOffset + BlobHeaderSize;

                return true;
            }
        }

        payloadOffset = 0;

        return false;
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> for a malformed DXBC container.
    /// </summary>
    /// <returns>This method always throws and never actually returns.</returns>
    private static int ThrowArgumentExceptionForInvalidContainer()
    {
        throw new ArgumentException("The input bytecode is not a well formed DXBC container.", "bytecode");
    }
}
