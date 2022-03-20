using System.Collections;

namespace Vesuv.Core.Collections
{
    public class MRU<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly LinkedList<T> _items;
        private uint _maxItems;

        public event EventHandler? ItemsChanged;

        public int Count => _items.Count;
        public bool IsReadOnly => false;

        public MRU(uint maxItems = 10)
        {
            if (maxItems == 0) {
                throw new ArgumentOutOfRangeException(nameof(maxItems), maxItems, "MaxItems must be a greater than 0");
            }
            _items = new LinkedList<T>();
            _maxItems = maxItems;
        }

        public MRU(ICollection<T> items)
        {
            if (items.Count == 0) {
                throw new ArgumentException("Items must contain at least one item", nameof(items));
            }
            _items = new LinkedList<T>(items);
            _maxItems = (uint)items.Count;
        }

        public MRU(uint maxItems, ICollection<T> items)
        {
            if (maxItems == 0) {
                throw new ArgumentOutOfRangeException(nameof(maxItems), maxItems, "MaxItems must be a greater than 0");
            }
            _items = new LinkedList<T>();
            var enumerator = items.GetEnumerator();
            var index = 0;
            while (index < maxItems && enumerator.MoveNext()) {
                _items.AddLast(enumerator.Current);
                ++index;
            }
            _maxItems = maxItems;
        }

        public void Add(T item)
        {
            var node = _items.Find(item);
            if (node != null) {
                _items.Remove(node);
                _items.AddFirst(node);
            } else {
                _items.AddFirst(item);
                if (_items.Count > _maxItems) {
                    _items.RemoveLast();
                }
            }
            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            _items.Clear();
            ItemsChanged?.Invoke(this, EventArgs.Empty);
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
            var success = _items.Remove(item);
            if (success) {
                ItemsChanged?.Invoke(this, EventArgs.Empty);
            }
            return success;
        }

        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
