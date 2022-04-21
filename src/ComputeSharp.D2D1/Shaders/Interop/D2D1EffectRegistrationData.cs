using System;
using System.Runtime.InteropServices;
using System.Text;
#if !NET6_0_OR_GREATER
using ComputeSharp.D2D1.NetStandard.System.Text;
#endif

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A helper type used to read and validate effect registration blobs produced
/// by <see cref="D2D1PixelShaderEffect.GetRegistrationBlob{T}(out Guid)"/>.
/// </summary>
/// <remarks>
/// <para>
/// The values in a <see cref="D2D1EffectRegistrationData"/> instance contain all necessary
/// information to register a new D2D1 effect by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
/// </para>
/// <para>
/// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.
/// </para>
/// </remarks>
public static class D2D1EffectRegistrationData
{
    /// <summary>
    /// The containing type for deserialized version 1 registration blobs.
    /// </summary>
    public unsafe readonly struct V1
    {
        /// <summary>
        /// The blob id for V1 registration data.
        /// </summary>
        internal static readonly Guid BlobId = new(0xA1B4A55E, 0x4C78, 0x413F, 0xA2, 0xE6, 0x0A, 0x02, 0xE6, 0x0A, 0x76, 0x18);

        /// <summary>
        /// Creates a new <see cref="V1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="classId">The effect class id.</param>
        /// <param name="numberOfInputs">The number of inputs for the effect.</param>
        /// <param name="propertyXml">The XML text that can be used to register the effect.</param>
        /// <param name="propertyBindings">The <see cref="D2D1PropertyBinding"/> values for the effect.</param>
        /// <param name="effectFactory">A pointer to the effect factory callback.</param>
        private V1(
            Guid classId,
            int numberOfInputs,
            string propertyXml,
            D2D1PropertyBinding[] propertyBindings,
            nint effectFactory)
        {
            ClassId = classId;
            NumberOfInputs = numberOfInputs;
            PropertyXml = propertyXml;
            PropertyBindings = propertyBindings;
            EffectFactory = (void*)effectFactory;
        }

        /// <summary>
        /// Gets the id to use to register the effect.
        /// </summary>
        public Guid ClassId { get; }

        /// <summary>
        /// Gets the number of inputs for the effect.
        /// </summary>
        public int NumberOfInputs { get; }

        /// <summary>
        /// Gets the XML text with the effect description, that can be used to register it.
        /// </summary>
        public string PropertyXml { get; }

        /// <summary>
        /// Gets the sequence of <see cref="D2D1PropertyBinding"/> values for the effect.
        /// </summary>
        public ReadOnlyMemory<D2D1PropertyBinding> PropertyBindings { get; }

        /// <summary>
        /// Gets a callback to an effect factory (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown**, HRESULT&gt;"/>).
        /// </summary>
        public void* EffectFactory { get; }

        /// <summary>
        /// Tries to load a <see cref="V1"/> instance from the input binary blob.
        /// </summary>
        /// <param name="blob">The input binary blob to deserialize.</param>
        /// <param name="data">The resulting <see cref="V1"/> instance, if successful.</param>
        /// <returns>Whether or not the input blob has been correctly deserialized (if it was not malformed).</returns>
        /// <remarks>
        /// The input blob should have been created by a call to any of the overloads
        /// of <see cref="D2D1PixelShaderEffect.GetRegistrationBlob{T}(out Guid)"/>.
        /// </remarks>
        public static unsafe bool TryLoad(ReadOnlyMemory<byte> blob, out V1 data)
        {
            data = default;

            if (blob.IsEmpty)
            {
                return false;
            }

            ReadOnlySpan<byte> span = blob.Span;

            // Blob id
            if (!MemoryMarshal.TryRead(span, out Guid blobId) ||
                blobId != BlobId)
            {
                return false;
            }

            span = span.Slice(sizeof(Guid));

            // Effect class id
            if (!MemoryMarshal.TryRead(span, out Guid effectId))
            {
                return false;
            }

            span = span.Slice(sizeof(Guid));

            // Number of inputs
            if (!MemoryMarshal.TryRead(span, out int numberOfInputs) ||
                numberOfInputs < 0)
            {
                return false;
            }

            span = span.Slice(sizeof(int));

            // Effect property XML
            int lengthOfXml = span.IndexOf((byte)'\0');

            if (lengthOfXml == -1)
            {
                return false;
            }

            string xml = Encoding.UTF8.GetString(span.Slice(0, lengthOfXml));

            span = span.Slice(lengthOfXml + 1);

            // Number of bindings
            if (!MemoryMarshal.TryRead(span, out int numberOfBindings) ||
                numberOfBindings < 0)
            {
                return false;
            }

            span = span.Slice(sizeof(int));

            D2D1PropertyBinding[] propertyBindings = new D2D1PropertyBinding[numberOfBindings];

            // Property bindings
            for (int i = 0; i < numberOfBindings; i++)
            {
                // Property name
                int lengthOfName = span.IndexOf((byte)'\0');

                if (lengthOfName == -1)
                {
                    return false;
                }

                string name = Encoding.UTF8.GetString(span.Slice(0, lengthOfName));

                span = span.Slice(lengthOfName + 1);

                // Property get function
                if (!MemoryMarshal.TryRead(span, out nint getFunction))
                {
                    return false;
                }

                span = span.Slice(sizeof(nint));

                // Property set function
                if (!MemoryMarshal.TryRead(span, out nint setFunction))
                {
                    return false;
                }

                span = span.Slice(sizeof(nint));

                propertyBindings[i] = new D2D1PropertyBinding(name, (void*)getFunction, (void*)setFunction);
            }

            // Effect factory
            if (!MemoryMarshal.TryRead(span, out nint effectFactory))
            {
                return false;
            }

            span = span.Slice(sizeof(nint));

            // If the buffer is bigger than expected, also consider it malformed
            if (span.Length > 0)
            {
                return false;
            }

            data = new V1(
                effectId,
                numberOfInputs,
                xml,
                propertyBindings,
                effectFactory);

            return true;
        }
    }
}
