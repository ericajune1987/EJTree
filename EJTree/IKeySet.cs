using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EJTree
{
    public interface IKeySet<T> : IEnumerable<T> where T : IComparable
    {
        public T FirstKey();
        public T LastKey();
        public int Size();
        public Boolean Empty();
        public Boolean ContainsKey(T key);
        public Boolean AddKey(T key, Object? data = null);
        public Boolean RemoveKey(T key);
    }
}
