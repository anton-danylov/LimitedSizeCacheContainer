using System;
using System.Collections.Generic;

namespace LimitedSizeCacheContainer
{
    public class LimitedSizeCacheContainer<T> : ILimitedSizeCacheContainer<T>
    {
        private readonly int _maxSize;
        
        private LinkedList<KeyValuePair<string, T>> _items = new LinkedList<KeyValuePair<string, T>>();
        private Dictionary<string, LinkedListNode<KeyValuePair<string, T>>> _itemsMap 
            = new Dictionary<string, LinkedListNode<KeyValuePair<string, T>>>();


        public int Count
        {
            get { return _items.Count; }
        }

        public LimitedSizeCacheContainer(int maxSize)
        {
            _maxSize = maxSize;
        }

        public void AddItem(string key, T item)
        {
            if (IsItemInCache(key))
            {
                throw new ArgumentException("Item with key specified is already in cache", nameof(key));
            }

            var listNode = _items.AddFirst(new KeyValuePair<string, T>(key, item));
            _itemsMap.Add(key, listNode);

            if (_items.Count > _maxSize)
            {
                _itemsMap.Remove(_items.Last.Value.Key);
                _items.RemoveLast();
            }
        }

        public T RetrieveItem(string key)
        {
            if (!IsItemInCache(key))
            {
                throw new ArgumentException("Item with key specified is not in cache", nameof(key));
            }

            var node = _itemsMap[key];
            _items.Remove(node);
            _items.AddFirst(node);

            return node.Value.Value;
        }

        public void RemoveItem(string key)
        {
            var node = _itemsMap[key];
            _items.Remove(node);
            _itemsMap.Remove(key);
        }

        public bool IsItemInCache(string key)
        {
            return _itemsMap.ContainsKey(key);
        }
    }

}