using System;
using System.Runtime.CompilerServices;

namespace DirectX12GameEngine.Shaders
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ShaderResourceAttribute : Attribute
    {
        public ShaderResourceAttribute([CallerLineNumber] int order = 0)
        {
            Order = order;
        }

        public int Order { get; }

        public bool Override { get; set; }
    }

    public class ConstantBufferResourceAttribute : ShaderResourceAttribute
    {
        public ConstantBufferResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class SamplerResourceAttribute : ShaderResourceAttribute
    {
        public SamplerResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class TextureResourceAttribute : ShaderResourceAttribute
    {
        public TextureResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class UnorderedAccessViewResourceAttribute : ShaderResourceAttribute
    {
        public UnorderedAccessViewResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class StaticResourceAttribute : ShaderResourceAttribute
    {
        public StaticResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NumThreadsAttribute : Attribute
    {
        public NumThreadsAttribute(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }
    }
}
