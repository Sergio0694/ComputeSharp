using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation;

namespace ComputeSharp.Shaders.Renderer
{
    /// <summary>
    /// A type that renders an HLSL template from the previously loaded shader data.
    /// </summary>
    internal static class HlslShaderRenderer
    {
        /// <summary>
        /// Renders a new HLSL shader source with the given parameters.
        /// </summary>
        /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
        /// <param name="info">The input <see cref="IShaderLoader"/> instance with the shader information.</param>
        /// <returns>The source code for the new HLSL shader.</returns>
        [Pure]
        public static ArrayPoolStringBuilder Render(int threadsX, int threadsY, int threadsZ, IShaderLoader info)
        {
            var builder = ArrayPoolStringBuilder.Create();

            // Header
            builder.AppendLine("// ================================================");
            builder.AppendLine("//                  AUTO GENERATED");
            builder.AppendLine("// ================================================");
            builder.AppendLine("// This shader was created by ComputeSharp.");
            builder.AppendLine("// See: https://github.com/Sergio0694/ComputeSharp.");

            // Group size constants
            builder.AppendLine();
            builder.Append("#define __GroupSize__get_X ");
            builder.AppendLine(threadsX.ToString());
            builder.Append("#define __GroupSize__get_Y ");
            builder.AppendLine(threadsY.ToString());
            builder.Append("#define __GroupSize__get_Z ");
            builder.AppendLine(threadsZ.ToString());

            // Define declarations
            foreach (var define in info.DefinesInfo)
            {
                builder.Append("#define ");
                builder.Append(define.Key);
                builder.Append(' ');
                builder.AppendLine(define.Value);
            }

            // Static fields
            if (info.StaticFields.Count > 0)
            {
                builder.AppendLine();

                foreach (var constant in info.StaticFields)
                {
                    builder.Append(constant.Value.TypeDeclaration);
                    builder.Append(' ');
                    builder.Append(constant.Key);

                    if (constant.Value.Assignment is string assignment)
                    {
                        builder.Append(" = ");
                        builder.Append(assignment);
                    }

                    builder.AppendLine(";");
                }
            }

            // Declared types
            foreach (var type in info.DeclaredTypes)
            {
                builder.AppendLine();
                builder.AppendLine(type);
            }

            // Captured variables
            builder.AppendLine();
            builder.AppendLine("cbuffer _ : register(b0)");
            builder.AppendLine('{');
            builder.AppendLine("    uint __x;");
            builder.AppendLine("    uint __y;");
            builder.AppendLine("    uint __z;");

            foreach (var local in info.FieldsInfo)
            {
                builder.Append("    ");
                builder.Append(local.FieldType);
                builder.Append(' ');
                builder.Append(local.FieldName);
                builder.AppendLine(';');
            }

            builder.AppendLine('}');

            // Buffers
            foreach (var buffer in info.HlslResourceInfo)
            {
                builder.AppendLine();

                switch (buffer)
                {
                    // Constant buffer go to cbuffer fields with a dummy local
                    case HlslResourceInfo.Constant _:
                        builder.Append("cbuffer _");
                        builder.Append(buffer.FieldName);
                        builder.Append(" : register(b");
                        builder.Append(buffer.BufferIndex.ToString());
                        builder.AppendLine(')');
                        builder.AppendLine('{');
                        builder.Append("    ");
                        builder.Append(buffer.FieldType);
                        builder.Append(' ');
                        builder.Append(buffer.FieldName);
                        builder.AppendLine("[2];");
                        builder.AppendLine('}');
                        break;

                    // Structured buffer have the same syntax but different id
                    case HlslResourceInfo.ReadOnly _:
                        char registerId = 't';
                        goto StructuredBuffer;
                    case HlslResourceInfo.ReadWrite _:
                        registerId = 'u';
                        StructuredBuffer:
                        builder.Append(buffer.FieldType);
                        builder.Append(' ');
                        builder.Append(buffer.FieldName);
                        builder.Append(" : register(");
                        builder.Append(registerId);
                        builder.Append(buffer.BufferIndex.ToString());
                        builder.AppendLine(");");
                        break;
                }
            }

            // Shared buffers
            foreach (var buffer in info.SharedBuffers)
            {
                builder.AppendLine();
                builder.Append("groupshared ");
                builder.Append(buffer.Value.Type);
                builder.Append(' ');
                builder.Append(buffer.Key);
                builder.Append('[');

                if (buffer.Value.Count is int count) builder.Append(count.ToString());
                else builder.Append((threadsX * threadsY * threadsZ).ToString());

                builder.AppendLine("];");
            }

            // Forward declarations
            foreach (var declaration in info.ForwardDeclarations)
            {
                builder.AppendLine();
                builder.AppendLine(declaration);
            }

            // Captured methods
            foreach (var function in info.MethodsInfo)
            {
                builder.AppendLine();
                builder.AppendLine(function);
            }

            // Entry point
            builder.AppendLine();
            builder.AppendLine("[NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]");
            builder.AppendLine(info.EntryPoint);

            return builder;
        }

        /// <summary>
        /// A helper type that implements a pooled buffer writer for <see cref="char"/> values.
        /// </summary>
        [DebuggerTypeProxy(typeof(DebugView))]
        public ref struct ArrayPoolStringBuilder
        {
            /// <summary>
            /// The underlying <see cref="char"/> array.
            /// </summary>
            private char[] array;

            /// <summary>
            /// The starting offset within <see cref="array"/>.
            /// </summary>
            private int index;

            /// <summary>
            /// Creates a new <see cref="ArrayPoolStringBuilder"/> instance ready to use.
            /// </summary>
            /// <returns>A new <see cref="ArrayPoolStringBuilder"/> instance with default values.</returns>
            [Pure]
            public static ArrayPoolStringBuilder Create()
            {
                ArrayPoolStringBuilder builder;

                builder.array = ArrayPool<char>.Shared.Rent(1024);
                builder.index = 0;

                return builder;
            }

            /// <inheritdoc/>
            public ReadOnlySpan<char> WrittenSpan
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => new(this.array, 0, this.index);
            }

            /// <summary>
            /// Appends the input sequence of characters to the current buffer.
            /// </summary>
            /// <param name="value">The input characters to write.</param>
            public void Append(string value)
            {
                EnsureCapacity(value.Length);

                value.AsSpan().CopyTo(this.array.AsSpan(this.index));

                this.index += value.Length;
            }

            /// <summary>
            /// Appends a character to the current buffer.
            /// </summary>
            public void Append(char character)
            {
                EnsureCapacity(1);

                this.array[this.index++] = character;
            }

            /// <summary>
            /// Appends the input sequence of characters to the current buffer, with a trailing new line.
            /// </summary>
            /// <param name="value">The input characters to write.</param>
            public void AppendLine(string value)
            {
                EnsureCapacity(value.Length + 1);

                value.AsSpan().CopyTo(this.array.AsSpan(this.index));

                this.index += value.Length;

                this.array[this.index++] = '\n';
            }

            /// <summary>
            /// Appends a character and a new line to the current buffer.
            /// </summary>
            public void AppendLine(char character)
            {
                EnsureCapacity(2);

                this.array[this.index++] = character;
                this.array[this.index++] = '\n';
            }

            /// <summary>
            /// Appends a new line to the current buffer.
            /// </summary>
            public void AppendLine()
            {
                EnsureCapacity(1);

                this.array[this.index++] = '\n';
            }

            /// <summary>
            /// Ensures that <see cref="array"/> has enough free space to contain a given number of new items.
            /// </summary>
            /// <param name="requestedSize">The minimum number of items to ensure space for in <see cref="array"/>.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void EnsureCapacity(int requestedSize)
            {
                if (requestedSize > this.array.Length - this.index)
                {
                    ResizeBuffer(requestedSize);
                }
            }

            /// <summary>
            /// Resizes <see cref="array"/> to ensure it can fit the specified number of new items.
            /// </summary>
            /// <param name="sizeHint">The minimum number of items to ensure space for in <see cref="array"/>.</param>
            [MethodImpl(MethodImplOptions.NoInlining)]
            private void ResizeBuffer(int sizeHint)
            {
                int minimumSize = this.index + sizeHint;

                // Rent a new array with the specified size, and copy as many items from the current array
                // as possible to the new array. This mirrors the behavior of the Array.Resize API from
                // the BCL: if the new size is greater than the length of the current array, copy all the
                // items from the original array into the new one. Otherwise, copy as many items as possible,
                // until the new array is completely filled, and ignore the remaining items in the first array.
                char[] newArray = ArrayPool<char>.Shared.Rent(minimumSize);
                int itemsToCopy = Math.Min(this.array.Length, minimumSize);

                Array.Copy(this.array, 0, newArray, 0, itemsToCopy);

                ArrayPool<char>.Shared.Return(array);

                this.array = newArray;
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                ArrayPool<char>.Shared.Return(this.array);
            }

            /// <summary>
            /// A debug proxy used for displaying debug info.
            /// </summary>
            internal sealed class DebugView
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="DebugView"/> class with the specified parameters.
                /// </summary>
                /// <param name="builder">The input <see cref="ArrayPoolStringBuilder"/> instance to display.</param>
                public DebugView(ArrayPoolStringBuilder builder)
                {
                    Text = builder.WrittenSpan.ToString();
                }

                /// <summary>
                /// Gets the text to display for the current instance.
                /// </summary>
                public string Text { get; }
            }
        }
    }
}
