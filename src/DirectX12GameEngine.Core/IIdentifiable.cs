using System;

namespace DirectX12GameEngine.Core
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
