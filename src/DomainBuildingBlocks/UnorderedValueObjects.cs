using System.Collections;
using System.Collections.Generic;

namespace DomainBuildingBlocks
{
    public class UnorderedValueObjects<T> : ValueObject<T>, ICollection<T> where T : class
    {
        private readonly ICollection<T> _items;
        
        public UnorderedValueObjects()
        {
            _items = new List<T>();
        }

        protected UnorderedValueObjects(ICollection<T> collection)
        {
            _items = collection;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;
    }
}