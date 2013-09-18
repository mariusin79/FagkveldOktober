using System;
using System.Collections;
using System.Configuration;

namespace Utilities
{
    public class ConnectionStringsWrapper : IDictionary
    {
        private readonly ConnectionStringSettingsCollection _connections;

        public ConnectionStringsWrapper(ConnectionStringSettingsCollection connections)
        {
            _connections = connections;
        }

        public bool Contains(object key)
        {
            throw new NotImplementedException();
        }

        public void Add(object key, object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public object this[object key]
        {
            get
            {
                return _connections[key as string].ConnectionString;
            }
            set { }
        }

        public ICollection Keys { get; private set; }
        public ICollection Values { get; private set; }
        public bool IsReadOnly { get; private set; }
        public bool IsFixedSize { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }
        public object SyncRoot { get; private set; }
        public bool IsSynchronized { get; private set; }
    }
}