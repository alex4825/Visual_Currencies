using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.Other
{
    public class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly Transform _parent;

        public ObjectPool(T prefab, int initialSize = 10, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialSize; i++)
                AddObjectToPool();
        }

        public T Get(Vector3 position)
        {
            if (_pool.Count == 0)
                AddObjectToPool();

            var obj = _pool.Dequeue();
            obj.transform.position = position;
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        private T AddObjectToPool()
        {
            var obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
            return obj;
        }
    }

}