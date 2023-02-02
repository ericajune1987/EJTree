using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EJTree
{
    internal class TreeNode<T> : ITreeNode<T> where T : IComparable
    {
        public TreeNode()
        {
            m_keyspace = new KeySet<T>();
            m_nodes = new List<ITreeNode<T>>();
        }

        public ITreeNode<T>? FindKeySpace(T key)
        {
            return null;
        }

        public T FirstKey()
        {
            return m_keyspace.FirstKey();
        }

        public T LastKey()
        {
            return m_keyspace.LastKey();
        }

        public int Size()
        {
            return m_keyspace.Size();
        }

        public Boolean Empty()
        {
            return m_keyspace.Empty();
        }

        public Boolean ContainsKey(T key)
        {
            return m_keyspace.Empty();
        }

        public Boolean AddKey(T key, Object? data = null)
        {
            return m_keyspace.AddKey(key, data);
        }

        public Boolean RemoveKey(T key)
        {
            return m_keyspace.RemoveKey(key);
        }

        private readonly KeySet<T> m_keyspace;
        private readonly List<ITreeNode<T>> m_nodes;
    }
}
