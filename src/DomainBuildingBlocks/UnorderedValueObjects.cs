using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DomainBuildingBlocks
{
    public class UnorderedValueObjects<T> : ICollection<T>, IEquatable<UnorderedValueObjects<T>> where T : class
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
        
        /// <summary>
        /// Compares two Unordered Value Objects according to the values of all value objects.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if objects are considered equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as UnorderedValueObjects<T>;

            return Equals(other);
        }

        /// <summary>
        /// Returns hashcode value calculated according to the values of all value objects.        
        /// </summary>
        /// <returns>Hashcode value.</returns>
        public override int GetHashCode()
        {
            const int startValue = 17;
            const int multiplier = 59;

            int hashCode = startValue;

            foreach (var item in _items)
            {
                hashCode = hashCode * multiplier + item.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Compares two Unordered Value Objects according to the values of all fields in objects.
        /// </summary>
        /// <param name="other">Object to compare to.</param>
        /// <returns>True if objects are considered equal.</returns>
        public bool Equals(UnorderedValueObjects<T> other)
        {
            if (other == null) return false;
            
            var itemsPair = _items.Zip(other._items,
                (itemThis, itemOther) => new {ItemThis = itemThis, ItemOther = itemOther});

            foreach (var itemPair in itemsPair)
            {
                if (!itemPair.ItemThis.Equals(itemPair.ItemOther))
                    return false;
            }
            
            return true;
        }

        public UnorderedValueObjects<T> Copy()
        {
            var destination = new T[_items.Count];
            _items.CopyTo(destination, 0);
            return new UnorderedValueObjects<T>(destination);
        }
    }
}