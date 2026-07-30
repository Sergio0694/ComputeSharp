using System;
using System.Buffers.Binary;

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <inheritdoc/>
partial class Dxbc
{
    /// <summary>
    /// The offset of the checksum in a DXBC container.
    /// </summary>
    private const int ChecksumOffset = 4;

    /// <summary>
    /// The size of the checksum in a DXBC container.
    /// </summary>
    private const int ChecksumSize = 16;

    /// <summary>
    /// Recomputes the checksum of a DXBC container in place.
    /// </summary>
    /// <param name="bytecode">The DXBC container to update.</param>
    /// <remarks>
    /// The checksum covers the whole container except for the signature and the checksum itself.
    /// </remarks>
    public static void UpdateChecksum(Span<byte> bytecode)
    {
        Span<uint> state = stackalloc uint[4] { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476 };

        ComputeHash(bytecode.Slice(ChecksumOffset + ChecksumSize), state);

        for (int i = 0; i < state.Length; i++)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(bytecode.Slice(ChecksumOffset + (i * sizeof(uint))), state[i]);
        }
    }

    /// <summary>
    /// Computes the hash used by DXBC containers over some input data.
    /// </summary>
    /// <param name="data">The data to hash.</param>
    /// <param name="state">The hash state to initialize and update.</param>
    /// <remarks>
    /// <para>
    /// This is the MD5 algorithm from RFC 1321 with a modified handling of the final block (or blocks): the
    /// length in bits is stored at the start of that block rather than at offset 56, the data is shifted by
    /// four bytes to make room for it, and the last four bytes hold <c>1 | (byteCount &lt;&lt; 1)</c> rather
    /// than the high half of the length. The transform itself is unchanged.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://microsoft.github.io/hlsl-specs/proposals/infra/inf-0004-validator-hashing/"/>.
    /// </para>
    /// </remarks>
    private static void ComputeHash(ReadOnlySpan<byte> data, Span<uint> state)
    {
        int byteCount = data.Length;
        int leftOver = byteCount & 0x3F;
        int padAmount;
        bool hasTwoRowsOfPadding;

        // The data is padded so that the final block has room for the trailing length
        if (leftOver < 56)
        {
            padAmount = 56 - leftOver;
            hasTwoRowsOfPadding = false;
        }
        else
        {
            padAmount = 120 - leftOver;
            hasTwoRowsOfPadding = true;
        }

        int blockCount = (byteCount + padAmount + 8) >> 6;
        int nextEndState = hasTwoRowsOfPadding ? blockCount - 2 : blockCount - 1;

        Span<byte> block = stackalloc byte[64];
        Span<uint> x = stackalloc uint[16];

        for (int i = 0, offset = 0; i < blockCount; i++, offset += 64)
        {
            if (i == nextEndState)
            {
                int remainder = byteCount - offset;

                // The padding is a single 0x80 byte followed by zeros, so clearing the block
                // upfront means only that first byte ever has to be written explicitly.
                block.Clear();

                if (!hasTwoRowsOfPadding && i == blockCount - 1)
                {
                    BinaryPrimitives.WriteUInt32LittleEndian(block, (uint)byteCount << 3);
                    data.Slice(offset, remainder).CopyTo(block.Slice(sizeof(uint)));

                    block[sizeof(uint) + remainder] = 0x80;

                    BinaryPrimitives.WriteUInt32LittleEndian(block.Slice(60), 1u | ((uint)byteCount << 1));
                }
                else if (i == blockCount - 2)
                {
                    data.Slice(offset, remainder).CopyTo(block);

                    block[remainder] = 0x80;

                    nextEndState = blockCount - 1;
                }
                else
                {
                    // The 0x80 byte was already written at the end of the previous block,
                    // so the rest of the padding in this one is just the zeros from above.
                    BinaryPrimitives.WriteUInt32LittleEndian(block, (uint)byteCount << 3);
                    BinaryPrimitives.WriteUInt32LittleEndian(block.Slice(60), 1u | ((uint)byteCount << 1));
                }

                LoadBlock(block, x);
            }
            else
            {
                LoadBlock(data.Slice(offset, 64), x);
            }

            Transform(state, x);
        }
    }

    /// <summary>
    /// Loads a 64 bytes block into the 16 words used by a single transform.
    /// </summary>
    /// <param name="block">The block to load.</param>
    /// <param name="x">The resulting words.</param>
    private static void LoadBlock(ReadOnlySpan<byte> block, Span<uint> x)
    {
        for (int i = 0; i < x.Length; i++)
        {
            x[i] = BinaryPrimitives.ReadUInt32LittleEndian(block.Slice(i * sizeof(uint)));
        }
    }

    /// <summary>
    /// Applies the four MD5 rounds for a single block to the hash state.
    /// </summary>
    /// <param name="state">The hash state to update.</param>
    /// <param name="x">The 16 words of the block being processed.</param>
    private static void Transform(Span<uint> state, ReadOnlySpan<uint> x)
    {
        uint a = state[0];
        uint b = state[1];
        uint c = state[2];
        uint d = state[3];

        // Round 1
        FF(ref a, b, c, d, x[0], 7, 0xD76AA478);
        FF(ref d, a, b, c, x[1], 12, 0xE8C7B756);
        FF(ref c, d, a, b, x[2], 17, 0x242070DB);
        FF(ref b, c, d, a, x[3], 22, 0xC1BDCEEE);
        FF(ref a, b, c, d, x[4], 7, 0xF57C0FAF);
        FF(ref d, a, b, c, x[5], 12, 0x4787C62A);
        FF(ref c, d, a, b, x[6], 17, 0xA8304613);
        FF(ref b, c, d, a, x[7], 22, 0xFD469501);
        FF(ref a, b, c, d, x[8], 7, 0x698098D8);
        FF(ref d, a, b, c, x[9], 12, 0x8B44F7AF);
        FF(ref c, d, a, b, x[10], 17, 0xFFFF5BB1);
        FF(ref b, c, d, a, x[11], 22, 0x895CD7BE);
        FF(ref a, b, c, d, x[12], 7, 0x6B901122);
        FF(ref d, a, b, c, x[13], 12, 0xFD987193);
        FF(ref c, d, a, b, x[14], 17, 0xA679438E);
        FF(ref b, c, d, a, x[15], 22, 0x49B40821);

        // Round 2
        GG(ref a, b, c, d, x[1], 5, 0xF61E2562);
        GG(ref d, a, b, c, x[6], 9, 0xC040B340);
        GG(ref c, d, a, b, x[11], 14, 0x265E5A51);
        GG(ref b, c, d, a, x[0], 20, 0xE9B6C7AA);
        GG(ref a, b, c, d, x[5], 5, 0xD62F105D);
        GG(ref d, a, b, c, x[10], 9, 0x02441453);
        GG(ref c, d, a, b, x[15], 14, 0xD8A1E681);
        GG(ref b, c, d, a, x[4], 20, 0xE7D3FBC8);
        GG(ref a, b, c, d, x[9], 5, 0x21E1CDE6);
        GG(ref d, a, b, c, x[14], 9, 0xC33707D6);
        GG(ref c, d, a, b, x[3], 14, 0xF4D50D87);
        GG(ref b, c, d, a, x[8], 20, 0x455A14ED);
        GG(ref a, b, c, d, x[13], 5, 0xA9E3E905);
        GG(ref d, a, b, c, x[2], 9, 0xFCEFA3F8);
        GG(ref c, d, a, b, x[7], 14, 0x676F02D9);
        GG(ref b, c, d, a, x[12], 20, 0x8D2A4C8A);

        // Round 3
        HH(ref a, b, c, d, x[5], 4, 0xFFFA3942);
        HH(ref d, a, b, c, x[8], 11, 0x8771F681);
        HH(ref c, d, a, b, x[11], 16, 0x6D9D6122);
        HH(ref b, c, d, a, x[14], 23, 0xFDE5380C);
        HH(ref a, b, c, d, x[1], 4, 0xA4BEEA44);
        HH(ref d, a, b, c, x[4], 11, 0x4BDECFA9);
        HH(ref c, d, a, b, x[7], 16, 0xF6BB4B60);
        HH(ref b, c, d, a, x[10], 23, 0xBEBFBC70);
        HH(ref a, b, c, d, x[13], 4, 0x289B7EC6);
        HH(ref d, a, b, c, x[0], 11, 0xEAA127FA);
        HH(ref c, d, a, b, x[3], 16, 0xD4EF3085);
        HH(ref b, c, d, a, x[6], 23, 0x04881D05);
        HH(ref a, b, c, d, x[9], 4, 0xD9D4D039);
        HH(ref d, a, b, c, x[12], 11, 0xE6DB99E5);
        HH(ref c, d, a, b, x[15], 16, 0x1FA27CF8);
        HH(ref b, c, d, a, x[2], 23, 0xC4AC5665);

        // Round 4
        II(ref a, b, c, d, x[0], 6, 0xF4292244);
        II(ref d, a, b, c, x[7], 10, 0x432AFF97);
        II(ref c, d, a, b, x[14], 15, 0xAB9423A7);
        II(ref b, c, d, a, x[5], 21, 0xFC93A039);
        II(ref a, b, c, d, x[12], 6, 0x655B59C3);
        II(ref d, a, b, c, x[3], 10, 0x8F0CCC92);
        II(ref c, d, a, b, x[10], 15, 0xFFEFF47D);
        II(ref b, c, d, a, x[1], 21, 0x85845DD1);
        II(ref a, b, c, d, x[8], 6, 0x6FA87E4F);
        II(ref d, a, b, c, x[15], 10, 0xFE2CE6E0);
        II(ref c, d, a, b, x[6], 15, 0xA3014314);
        II(ref b, c, d, a, x[13], 21, 0x4E0811A1);
        II(ref a, b, c, d, x[4], 6, 0xF7537E82);
        II(ref d, a, b, c, x[11], 10, 0xBD3AF235);
        II(ref c, d, a, b, x[2], 15, 0x2AD7D2BB);
        II(ref b, c, d, a, x[9], 21, 0xEB86D391);

        state[0] += a;
        state[1] += b;
        state[2] += c;
        state[3] += d;
    }

    /// <summary>
    /// Applies a single operation from the first MD5 round.
    /// </summary>
    /// <param name="a">The accumulator being updated.</param>
    /// <param name="b">The second state word.</param>
    /// <param name="c">The third state word.</param>
    /// <param name="d">The fourth state word.</param>
    /// <param name="x">The word of the block being mixed in.</param>
    /// <param name="s">The amount to rotate the accumulator by.</param>
    /// <param name="ac">The constant for this operation.</param>
    private static void FF(ref uint a, uint b, uint c, uint d, uint x, int s, uint ac)
    {
        a += ((b & c) | (~b & d)) + x + ac;
        a = RotateLeft(a, s) + b;
    }

    /// <inheritdoc cref="FF"/>
    private static void GG(ref uint a, uint b, uint c, uint d, uint x, int s, uint ac)
    {
        a += ((b & d) | (c & ~d)) + x + ac;
        a = RotateLeft(a, s) + b;
    }

    /// <inheritdoc cref="FF"/>
    private static void HH(ref uint a, uint b, uint c, uint d, uint x, int s, uint ac)
    {
        a += (b ^ c ^ d) + x + ac;
        a = RotateLeft(a, s) + b;
    }

    /// <inheritdoc cref="FF"/>
    private static void II(ref uint a, uint b, uint c, uint d, uint x, int s, uint ac)
    {
        a += (c ^ (b | ~d)) + x + ac;
        a = RotateLeft(a, s) + b;
    }

    /// <summary>
    /// Rotates a value to the left by a given amount.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The amount to rotate by.</param>
    /// <returns>The rotated value.</returns>
    private static uint RotateLeft(uint value, int offset)
    {
        return (value << offset) | (value >> (32 - offset));
    }
}
