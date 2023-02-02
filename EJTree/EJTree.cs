namespace EJTree
{
    public class EJTree<T> where T : IComparable
    {
        public EJTree(int order, Boolean allowDupeKeys, Boolean allowNullKeys)
        {
            m_order = order;
            m_allowDupeKeys = allowDupeKeys;
            m_allowNullKeys = allowNullKeys;

            m_root = new TreeNode<T>();
        }

        public Boolean ContainsKey(T key)
        {
            ITreeNode<T>? keySpace = FindKeySpace(key);

            if (keySpace == null)
            {
                return false;
            } else
            {
                return keySpace.ContainsKey(key);
            }
        }

        private ITreeNode<T>? FindKeySpace(T key)
        {
            return m_root.FindKeySpace(key);
        }

        private readonly int m_order;
        private readonly Boolean m_allowDupeKeys;
        private readonly Boolean m_allowNullKeys;

        private readonly ITreeNode<T> m_root;
    }
}