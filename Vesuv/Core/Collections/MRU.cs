using System.Collections;
using System.Collections.Specialized;

namespace Vesuv.Core.Collections
{
    public class MRU<T> : INotifyCollectionChanged, ICollection<T>, IEnumerable<T>, IEnumerable where T : IEquatable<T>
    {
        private readonly LinkedList<T> _items;
        private int _capacity;

        public int Capacity {
            get => _capacity;
            set {
                if (_capacity != value) {
                    if (value < 1) {
                        throw new ArgumentOutOfRangeException(nameof(value), value, "Value mut be greater than 0");
                    }
                    _capacity = value;
                    if (_items.Count > value) {
                        var node = _items.First;
                        for (int i = 0; i < value; ++i) {
                            node = node?.Next;
                        }
                        if (node != null) {
                            while (node.Next != null) {
                                _items.Remove(node.Next);
                            }
                            _items.Remove(node);
                        }
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                }
            }
        }

        public int Count => _items.Count;
        public bool IsReadOnly => false;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public MRU(int capacity = 10)
        {
            if (capacity < 1) {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than 0");
            }
            _items = new LinkedList<T>();
            _capacity = capacity;
        }

        public MRU(ICollection<T> items)
        {
            if (items.Count == 0) {
                throw new ArgumentException("Items must contain at least one item", nameof(items));
            }
            _items = new LinkedList<T>(items);
            _capacity = _items.Count;
        }

        public MRU(int capacity, ICollection<T> items)
        {
            if (capacity <1 ) {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than 0");
            }
            _items = new LinkedList<T>();

            var enumerator = items.Distinct().GetEnumerator();
            var index = 0;
            while (index < capacity && enumerator.MoveNext()) {
                _items.AddLast(enumerator.Current);
                ++index;
            }
            _capacity = capacity;
        }

        public void Add(T item)
        {
            var node = _items.Find(item);
            if (node != null) {
                _items.Remove(node);
                _items.AddFirst(node);
            } else {
                _items.AddFirst(item);
                if (_items.Count > _capacity) {
                    _items.RemoveLast();
                }
            }
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Clear()
        {
            _items.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            return success;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
