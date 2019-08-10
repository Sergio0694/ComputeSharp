using System;

namespace DirectX12GameEngine.Core
{
    public static class DisposableExtensions
    {
        public static T DisposeBy<T>(this T item, ICollector collector) where T : IDisposable
        {
            collector.Disposables.Add(item);
            return item;
        }
    }
}
