// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/uuids.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Win32;

internal static partial class Windows
{
    /// <summary>Retrieves the GUID of of a specified type.</summary>
    /// <typeparam name="T">The type to retrieve the GUID for.</typeparam>
    /// <returns>A <see cref="UuidOfType"/> value wrapping a pointer to the GUID data for the input type. This value can be either converted to a <see cref="Guid"/> pointer, or implicitly assigned to a <see cref="Guid"/> value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe UuidOfType __uuidof<T>()
        where T : unmanaged, IComObject
    {
        return new UuidOfType(T.IID);
    }

    /// <summary>A proxy type that wraps a pointer to GUID data. Values of this type can be implicitly converted to and assigned to <see cref="Guid"/>* or <see cref="Guid"/> parameters.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]

    public readonly unsafe ref struct UuidOfType
    {
        private readonly Guid* riid;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal UuidOfType(Guid* riid)
        {
            this.riid = riid;
        }

        /// <summary>Reads a <see cref="Guid"/> value from the GUID buffer for a given <see cref="UuidOfType"/> instance.</summary>
        /// <param name="guid">The input <see cref="UuidOfType"/> instance to read data for.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Guid(UuidOfType guid) => *guid.riid;

        /// <summary>Returns the <see cref="Guid"/>* pointer to the GUID buffer for a given <see cref="UuidOfType"/> instance.</summary>
        /// <param name="guid">The input <see cref="UuidOfType"/> instance to read data for.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Guid*(UuidOfType guid) => guid.riid;
    }
}