using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EJTree
{
    public class KeySet<T> : IKeySet<T> where T : IComparable
    {
        public KeySet() 
        {
            m_size = 0;
        }
        public KeySet(T key, Object? data = null) : this()
        {
            AddKey(key, data);
        }
        public T FirstKey()
        {
            return m_first!.GetKey();
        }

        public T LastKey()
        {
            return m_last!.GetKey();
        }

        public int Size()
        {
            return m_size;
        }

        public Boolean Empty()
        {
            return (m_first == null);
        }

        public Boolean ContainsKey(T key)
        {
            KeyNode? n = m_first;
            while((n != null) && (key.CompareTo(n.GetKey()) > 0))
            {
                n = n.GetNext();
            }

            if (n == null)
            {
                return false;
            }

            return (key.CompareTo(n.GetKey()) == 0);
        }

        public Boolean AddKey(T key, Object? data = null)
        {
            if (m_first == null)
            {
                m_first = new KeyNode(key, data);
                m_last = m_first;
            }
            else
            {

                KeyNode? n = m_first;
                while ((n != null) && (key.CompareTo(n.GetKey()) > 0))
                {
                    n = n.GetNext();
                }
                KeyNode add = new KeyNode(key, data);

                if (n == null)
                {
                    m_last!.SetNext(add);
                    add.SetPrev(m_last);
                    m_last = add;
                }
                else
                {
                    KeyNode? prev = n.GetPrev();
                    if (prev == null)
                    {
                        add.SetNext(n);
                        n.SetPrev(add);
                        m_first = add;
                    }
                    else
                    {
                        prev.SetNext(add);
                        add.SetPrev(prev);
                        add.SetNext(n);
                        n.SetPrev(add);
                    }
                }
            }

            m_size++;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            KeyNode? n = m_first;
            while(n != null)
            {
                yield return n.GetKey();
                n = n.GetNext();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private KeyNode? m_first;
        private KeyNode? m_last;
        private int m_size;

        private class KeyNode
        {
            public KeyNode(T key, Object? data = null)
            {
                this.m_key = key;
                this.m_data = data;
                this.m_next = null;
                this.m_prev = null;
            }

            public T GetKey()
            {
                return this.m_key;
            }

            public KeyNode? GetNext()
            {
                return m_next;
            }

            public void SetNext(KeyNode? next)
            {
                m_next = next;
            }

            public KeyNode? GetPrev()
            {
                return m_prev;
            }

            public void SetPrev(KeyNode? prev)
            {
                m_prev = prev;
            }

            private T m_key;
            private Object? m_data;

            private KeyNode? m_next;
            private KeyNode? m_prev;
        }
    }
}
