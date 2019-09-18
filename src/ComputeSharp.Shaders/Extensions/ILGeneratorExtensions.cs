using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
    /// <summary>
    /// A <see langword="class"/> that provides extension methods for the <see langword="ILGenerator"/> type
    /// </summary>
    internal static class ILGeneratorExtensions
    {
        /// <summary>
        /// Puts the appropriate <see langword="ldc.i4"/>, <see langword="conv.i"/> and <see langword="add"/> instructions to advance a reference onto the stream of instructions
        /// </summary>
        /// <param name="il">The input <see cref="ILGenerator"/> instance to use to emit instructions</param>
        /// <param name="offset">The offset to use to advance the current reference on top of the execution stack</param>
        public static void EmitAddByteOffset(this ILGenerator il, int offset)
        {
            // Push the offset to the stack
            if (offset > 8) il.Emit(OpCodes.Ldc_I4, offset);
            else
            {
                il.Emit(offset switch
                {
                    1 => OpCodes.Ldc_I4_1,
                    2 => OpCodes.Ldc_I4_2,
                    4 => OpCodes.Ldc_I4_4,
                    8 => OpCodes.Ldc_I4_8,
                    _ => throw new InvalidOperationException("Huh?")
                });
            }

            // Converts the offset to native int and adds to the address
            il.Emit(OpCodes.Conv_I);
            il.Emit(OpCodes.Add);
        }

        /// <summary>
        /// Puts the appropriate <see langword="stind"/> or <see langword="stobj"/> instruction to write to a reference onto the stream of instructions
        /// </summary>
        /// <param name="il">The input <see cref="ILGenerator"/> instance to use to emit instructions</param>
        /// <param name="type">The type of value being written to the current reference on top of the execution stack</param>
        public static void EmitStoreToAddress(this ILGenerator il, Type type)
        {
            il.Emit(Marshal.SizeOf(type) switch
            {
                // Use the faster op codes for sizes <= 8
                1 => OpCodes.Stind_I1,
                2 => OpCodes.Stind_I2,
                4 when type == typeof(float) => OpCodes.Stind_R4,
                4 => OpCodes.Stind_I4,
                8 when type == typeof(double) => OpCodes.Stind_R8,
                8 when type == typeof(long) || type == typeof(ulong) => OpCodes.Stind_I8,

                // Default to stobj for all other value types
                _ => OpCodes.Stobj
            });
        }
    }
}
