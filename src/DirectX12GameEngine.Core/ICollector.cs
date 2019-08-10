using System;
using System.Collections.Generic;

namespace DirectX12GameEngine.Core
{
    public interface ICollector
    {
        ICollection<IDisposable> Disposables { get; }
    }
}
