using System;
using System.Runtime.CompilerServices;

namespace DirectX12GameEngine.Shaders
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ShaderMethodAttribute : ShaderResourceAttribute
    {
        public ShaderMethodAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ShaderAttribute : Attribute
    {
        public ShaderAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
