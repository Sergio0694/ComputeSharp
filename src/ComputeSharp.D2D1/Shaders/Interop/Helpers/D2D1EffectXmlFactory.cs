using System;
using System.Text;
using System.Threading;
using ComputeSharp.D2D1.Descriptors;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A factory type to produce effect registration XML blobs.
/// </summary>
internal static unsafe class D2D1EffectXmlFactory
{
    /// <summary>
    /// The shared <see cref="StringBuilder"/> instance to use to build effect registration XML blobs.
    /// </summary>
    private static readonly StringBuilder XmlBuilder = new(4096);

    /// <summary>
    /// Gets a <see cref="EffectXml"/> instance with the effect registration XML blob for a given shader type.
    /// </summary>
    /// <typeparam name="T">The type of shader type to build the XML for.</typeparam>
    /// <returns>The <see cref="EffectXml"/> instance with the effect registration XML blob.</returns>
    public static EffectXml GetXmlBuffer<T>()
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        StringBuilder builder = XmlBuilder;

        // We assume that the typical use of these APIs is not to ever try to concurrently register multiple
        // effects, also because D2D factories are not even thread-safe anyway. So we can just serialize calls
        // to the registration APIs and get away with a very simple and fast caching scheme, where we simply
        // use a single StringBuilder instance as temporary buffer to write the XML text to. The builder is
        // initialized with a size of 4096, which should be more than enough to hold pretty much all XML values.
        // When the returned Buffer value is disposed, the lock will be released.
        Monitor.Enter(builder);

        // Guard the XML building logic in a try/finally to avoid hanging
        try
        {
            _ = XmlBuilder.Clear();

            // Write the metadata properties for the effect
            _ = builder.Append($"""
                <?xml version='1.0'?>
                <Effect>
                    <Property name='DisplayName' type='string' value='{D2D1PixelShaderEffect.GetEffectDisplayName<T>()}'/>
                    <Property name='Description' type='string' value='{D2D1PixelShaderEffect.GetEffectDescription<T>()}'/>
                    <Property name='Category' type='string' value='{D2D1PixelShaderEffect.GetEffectCategory<T>()}'/>
                    <Property name='Author' type='string' value='{D2D1PixelShaderEffect.GetEffectAuthor<T>()}'/>
                    <Inputs>
                """);

            // Add the input nodes, if any
            for (int i = 0; i < D2D1PixelShader.GetInputCount<T>(); i++)
            {
                _ = builder.Append('\n');
                _ = builder.Append($"        <Input name='Source{i}'/>");
            }

            // Close the inputs tag and add all effect properties
            _ = builder.Append('\n');
            _ = builder.Append("""
                    </Inputs>
                    <Property name='ConstantBuffer' type='blob'>
                        <Property name='DisplayName' type='string' value='ConstantBuffer'/>
                    </Property>
                    <Property name='ResourceTextureManager0' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager0'/>
                    </Property>
                    <Property name='ResourceTextureManager1' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager1'/>
                    </Property>
                    <Property name='ResourceTextureManager2' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager2'/>
                    </Property>
                    <Property name='ResourceTextureManager3' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager3'/>
                    </Property>
                    <Property name='ResourceTextureManager4' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager4'/>
                    </Property>
                    <Property name='ResourceTextureManager5' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager5'/>
                    </Property>
                    <Property name='ResourceTextureManager6' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager6'/>
                    </Property>
                    <Property name='ResourceTextureManager7' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager7'/>
                    </Property>
                    <Property name='ResourceTextureManager8' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager8'/>
                    </Property>
                    <Property name='ResourceTextureManager9' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager9'/>
                    </Property>
                    <Property name='ResourceTextureManager10' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager10'/>
                    </Property>
                    <Property name='ResourceTextureManager11' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager11'/>
                    </Property>
                    <Property name='ResourceTextureManager12' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager12'/>
                    </Property>
                    <Property name='ResourceTextureManager13' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager13'/>
                    </Property>
                    <Property name='ResourceTextureManager14' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager14'/>
                    </Property>
                    <Property name='ResourceTextureManager15' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager15'/>
                    </Property>
                    <Property name='TransformMapper' type='iunknown'>
                        <Property name='DisplayName' type='string' value='TransformMapper'/>
                    </Property>
                </Effect>
                """);

            // Null terminator for the text
            _ = builder.Append('\0');
        }
        catch
        {
            // Something went wrong building the XML, so we have to release the lock early
            Monitor.Exit(builder);
        }

        return new(builder);
    }

    /// <summary>
    /// A simple buffer for an effect XML blob.
    /// </summary>
    public readonly ref struct EffectXml
    {
        /// <summary>
        /// The <see cref="StringBuilder"/> instance holding the registration blob.
        /// </summary>
        private readonly StringBuilder builder;

        /// <summary>
        /// Creates a new <see cref="EffectXml"/> value with the specified parameters.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> instance with the effect XML.</param>
        [Obsolete($"This should only be used by {nameof(D2D1EffectXmlFactory)}.")]
        public EffectXml(StringBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> for the buffer (pretty much always with no allocation).
        /// </summary>
        /// <returns>The <see cref="ReadOnlySpan{T}"/> value with the effect XML blob.</returns>
        public ReadOnlySpan<char> GetOrAllocateBuffer()
        {
#if NET6_0_OR_GREATER
            StringBuilder.ChunkEnumerator enumerator = this.builder.GetChunks();

            if (enumerator.MoveNext())
            {
                ReadOnlyMemory<char> segment = enumerator.Current;

                if (!enumerator.MoveNext())
                {
                    return segment.Span;
                }
            }
#endif

            return this.builder.ToString().AsSpan();
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Monitor.Exit(this.builder);
        }
    }
}