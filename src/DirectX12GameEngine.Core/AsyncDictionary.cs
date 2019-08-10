using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectX12GameEngine.Core
{
    public class AsyncDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public event EventHandler<DictionaryChangedEventArgs<TKey, TValue>> ItemAdded;

        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
            ItemAdded?.Invoke(this, new DictionaryChangedEventArgs<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public bool ContainsValue(TValue value) => dictionary.ContainsValue(value);

        public async Task<TValue> GetValueAsync(TKey key)
        {
            if (!dictionary.TryGetValue(key, out TValue value))
            {
                TaskCompletionSource<TValue> tcs = new TaskCompletionSource<TValue>();

                void OnItemAdded(object s, DictionaryChangedEventArgs<TKey, TValue> e)
                {
                    if (e.Item.Key.Equals(key))
                    {
                        tcs.TrySetResult(e.Item.Value);
                    }
                }

                ItemAdded += OnItemAdded;
                value = await tcs.Task;
                ItemAdded -= OnItemAdded;
            }

            return value;
        }

        public Task<TValue> this[TKey key] => GetValueAsync(key);
    }

    public class DictionaryChangedEventArgs<TKey, TValue> : EventArgs
    {
        public DictionaryChangedEventArgs(TKey key, TValue value)
            : this(new KeyValuePair<TKey, TValue>(key, value))
        {
        }

        public DictionaryChangedEventArgs(KeyValuePair<TKey, TValue> item)
        {
            Item = item;
        }

        public KeyValuePair<TKey, TValue> Item { get; }
    }
}
