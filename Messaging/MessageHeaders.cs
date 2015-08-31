using System;
using System.Collections;
using System.Collections.Generic;

namespace Cloudmark.Messaging
{

    public class MessageHeaders : IDictionary<string, object>
    {
        public static readonly Guid ID_VALUE_NONE = Guid.Empty;
        public static readonly string ID = "id";
        public static readonly string TIMESTAMP = "timestamp";
        public static readonly string CONTENT_TYPE = "contentType";
        public static readonly string REPLY_CHANNEL = "replyChannel";
        public static readonly string ERROR_CHANNEL = "errorChannel";
        private readonly Dictionary<string, object> headers;
        
        private static readonly IdGenerator 


        public MessageHeaders(IDictionary<string, object> headers) : this(headers, null, null)
        {
        }

        public MessageHeaders(IDictionary<string, object> headers, Guid? guid, long? timestamp)
        {
            this.headers = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> entry in headers)
            {
                this.headers.Add(entry.Key, entry.Value);
            }
            
            if (guid == null || guid == ID_VALUE_NONE){
                this.headers.Remove(ID);
            } else {
                this.headers.Add(ID, guid);
            }

            if (timestamp == null)
            {
                this.headers.Add(TIMESTAMP, DateTime.Now.Ticks);
            }
            else if (timestamp < 0)
            {
                this.headers.Remove(TIMESTAMP);
            }
            else
            {
                this.headers.Add(TIMESTAMP, timestamp);
            }

        }
        
        protected Dictionary<string, object> GetRawHeaders(){ 
            return this.headers; 
        }
        
        

        public object this[string key]
        {
            get
            {
                return this.headers[key];
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        public int Count
        {
            get
            {
                return this.headers.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return this.headers.Keys;
            }
        }

        public ICollection<object> Values
        {
            get
            {
                return this.headers.Values;
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();

        }

        public void Add(string key, object value)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.headers.ContainsKey(item.Key) && item.Value.Equals(this.headers[item.Key]);
        }

        public bool ContainsKey(string key)
        {
            return this.headers.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotSupportedException(); 
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.headers.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();
        }

        public bool Remove(string key)
        {
            throw new NotSupportedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            return this.headers.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
        return this.headers.GetEnumerator();
        }
    }
}
