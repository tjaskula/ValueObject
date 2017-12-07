using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DomainBuildingBlocks
{
    public class OrderedValueObjects<T> : ICollection<T>, IEquatable<OrderedValueObjects<T>> where T : class
    {
        private readonly Comparer<T> _comparer;
        private readonly List<T> _items;

        private OrderedValueObjects(List<T> list) : this(Comparer<T>.Default)
        {
            _items = list;
        }
        
        public OrderedValueObjects() : this(Comparer<T>.Default)
        {
        }

        public OrderedValueObjects(Comparer<T> comparer)
        {
            _items = new List<T>();
            _comparer = comparer;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            int index = _items.BinarySearch(item, _comparer);
            _items.Insert(index >= 0 ? index : ~index, item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.BinarySearch(item, _comparer) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            int index = _items.BinarySearch(item, _comparer);
            if (index >= 0)
            {
                _items.RemoveAt(index);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Compares two Ordered Value Objects according to the values of all value objects.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if objects are considered equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as OrderedValueObjects<T>;

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
        public bool Equals(OrderedValueObjects<T> other)
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
        
        /// <summary>
        /// Compares two value objects for equality.
        /// </summary>
        /// <param name="x">The first value object.</param>
        /// <param name="y">The second value object.</param>
        /// <returns>Returns true if two object are the same.</returns>
        public static bool operator ==(OrderedValueObjects<T> x, OrderedValueObjects<T> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);

            return x.Equals(y);
        }

        /// <summary>
        /// Compares two value objects for inequlaity.
        /// </summary>
        /// <param name="x">The first value object.</param>
        /// <param name="y">The second value object.</param>
        /// <returns>Returns true if two object are not the same.</returns>
        public static bool operator !=(OrderedValueObjects<T> x, OrderedValueObjects<T> y)
        {
            return !(x == y);
        }

        public OrderedValueObjects<T> Copy()
        {
            var destination = new T[_items.Count];
            _items.CopyTo(destination, 0);
            return new OrderedValueObjects<T>(destination.ToList());
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;
    }
}