using System;
using System.Buffers;
using System.Text;
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
        // Compute all necessary values first, outside of the lock
        string? displayName = D2D1PixelShaderEffect.GetEffectDisplayName<T>();
        string? description = D2D1PixelShaderEffect.GetEffectDescription<T>();
        string? category = D2D1PixelShaderEffect.GetEffectCategory<T>();
        string? author = D2D1PixelShaderEffect.GetEffectAuthor<T>();
        int inputCount = D2D1PixelShader.GetInputCount<T>();
        int resourceTextureCount = D2D1PixelShader.GetResourceTextureCount<T>();

        StringBuilder builder = XmlBuilder;

        // We use a simple caching schema here with a single StringBuilder instance shared across all threads.
        // It is only used to write the effect XML blob and copy it, which is extremely fast. The lock itself
        // is not taken during the actual effect registration, but only to build the effect XML text. The builder
        // is initialized with a size of 4096, which should be more than enough to hold pretty much all XML values.
        lock (builder)
        {
            _ = builder.Clear();

            // Write the metadata properties for the effect
            _ = builder.Append($"""
                <?xml version='1.0'?>
                <Effect>
                    <Property name='DisplayName' type='string' value='{displayName}'/>
                    <Property name='Description' type='string' value='{description}'/>
                    <Property name='Category' type='string' value='{category}'/>
                    <Property name='Author' type='string' value='{author}'/>
                    <Inputs>
                """);

            // Add the input nodes, if any
            for (int i = 0; i < inputCount; i++)
            {
                _ = builder.Append('\n');
                _ = builder.Append($"        <Input name='Source{i}'/>");
            }

            // Close the inputs tag and add the always available properties
            _ = builder.Append('\n');
            _ = builder.Append("""
                    </Inputs>
                    <Property name='ConstantBuffer' type='blob'>
                        <Property name='DisplayName' type='string' value='ConstantBuffer'/>
                    </Property>
                    <Property name='TransformMapper' type='iunknown'>
                        <Property name='DisplayName' type='string' value='TransformMapper'/>
                    </Property>
                """);

            // Add the resource texture manager nodes, if any
            for (int i = 0; i < resourceTextureCount; i++)
            {
                _ = builder.Append('\n');
                _ = builder.Append($"""
                    <Property name='ResourceTextureManager{i}' type='iunknown'>
                        <Property name='DisplayName' type='string' value='ResourceTextureManager{i}'/>
                    </Property>
                """);
            }

            // Close the effect tag
            _ = builder.Append('\n');
            _ = builder.Append("</Effect>");

            // Null terminator for the text
            _ = builder.Append('\0');

            return new(builder);
        }
    }

    /// <summary>
    /// A simple buffer for an effect XML blob.
    /// </summary>
    public readonly ref struct EffectXml
    {
        /// <summary>
        /// The <see cref="char"/> buffer with the effect XML blob.
        /// </summary>
        private readonly char[] buffer;

        /// <summary>
        /// The length of the effect XML blob.
        /// </summary>
        private readonly int length;

        /// <summary>
        /// Creates a new <see cref="EffectXml"/> value with the specified parameters.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> instance with the effect XML.</param>
        [Obsolete($"This should only be used by {nameof(D2D1EffectXmlFactory)}.")]
        public EffectXml(StringBuilder builder)
        {
            // Rent a buffer from the pool with the current builder size
            this.buffer = ArrayPool<char>.Shared.Rent(builder.Length);
            this.length = builder.Length;

            // Copy the current builder content with the effect XML blob into the
            // rented buffer. This allows the builder to be reused for other effects.
            builder.CopyTo(0, this.buffer, 0, builder.Length);
        }

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> for the effect XML blob.
        /// </summary>
        /// <returns>The <see cref="ReadOnlySpan{T}"/> value with the effect XML blob.</returns>
        public ReadOnlySpan<char> GetBuffer()
        {
            return this.buffer.AsSpan(0, this.length);
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            ArrayPool<char>.Shared.Return(this.buffer);
        }
    }
}