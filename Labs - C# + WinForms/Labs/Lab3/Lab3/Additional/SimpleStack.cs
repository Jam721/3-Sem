using System.Collections;

namespace Lab3.Additional
{
    public class SimpleStackItems<T>
    {
        public T? Element { get; set; }
        public SimpleStackItems<T>? Next { get; set; }
    }

    public class SimpleStack<T> : IEnumerable<T>
    {
        private SimpleStackItems<T>? _head;

        public bool IsEmpty => _head == null;

        public void Add(T value)
        {
            var newItem = new SimpleStackItems<T> { Element = value, Next = _head };
            _head = newItem;
        }

        public T Pop()
        {
            if (IsEmpty) throw new InvalidOperationException("Сьек пуст");

            var result = _head!.Element;
            _head = _head.Next;

            return result ?? throw new InvalidOperationException();
        }

        public override string ToString()
        {
            string s = "";
            foreach (var item in this)
            {
                s += item + " ";
            }
            return s;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                if (current.Element != null) yield return current.Element;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}