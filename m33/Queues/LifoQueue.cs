using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m33.Queues
{
    public class LifoQueue<T>
    {
        private List<T> _objectList = new List<T>();

        public void Enqueue(T obj)
        {
            _objectList.Add(obj);
        }

        public T Dequeue()
        {
            T obj = _objectList[_objectList.Count - 1];
            _objectList.Remove(obj);
            return obj;
        }

        public void Clear()
        {
            _objectList.Clear();
        }

        public int Count()
        {
            return _objectList.Count;
        }
    }
}
