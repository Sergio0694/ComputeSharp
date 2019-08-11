using System;

namespace DirectX12GameEngine.Shaders
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
    public abstract class ShaderSemanticAttribute : Attribute
    {
    }

    public abstract class ShaderSemanticWithIndexAttribute : ShaderSemanticAttribute
    {
        public ShaderSemanticWithIndexAttribute(int index = 0)
        {
            Index = index;
        }

        public int Index { get; }
    }

    public class PositionSemanticAttribute : ShaderSemanticWithIndexAttribute
    {
        public PositionSemanticAttribute(int index = 0) : base(index)
        {
        }
    }

    public class NormalSemanticAttribute : ShaderSemanticWithIndexAttribute
    {
        public NormalSemanticAttribute(int index = 0) : base(index)
        {
        }
    }

    public class TextureCoordinateSemanticAttribute : ShaderSemanticWithIndexAttribute
    {
        public TextureCoordinateSemanticAttribute(int index = 0) : base(index)
        {
        }
    }

    public class ColorSemanticAttribute : ShaderSemanticWithIndexAttribute
    {
        public ColorSemanticAttribute(int index = 0) : base(index)
        {
        }
    }

    public class TangentSemanticAttribute : ShaderSemanticWithIndexAttribute
    {
        public TangentSemanticAttribute(int index = 0) : base(index)
        {
        }
    }

    public class SystemTargetSemanticAttribute : ShaderSemanticWithIndexAttribute
    {
        public SystemTargetSemanticAttribute(int index = 0) : base(index)
        {
        }
    }

    public class SystemDispatchThreadIdSemanticAttribute : ShaderSemanticAttribute
    {
    }

    public class SystemInstanceIdSemanticAttribute : ShaderSemanticAttribute
    {
    }

    public class SystemIsFrontFaceSemanticAttribute : ShaderSemanticAttribute
    {
    }

    public class SystemPositionSemanticAttribute : ShaderSemanticAttribute
    {
    }

    public class SystemRenderTargetArrayIndexSemanticAttribute : ShaderSemanticAttribute
    {
    }
}
