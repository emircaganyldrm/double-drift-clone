using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public abstract class PoolBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T objectPrefab;

        [SerializeField] protected int initialCount;

        protected Queue<T> poolQueue = new();

        public void GeneratePool(T prefab, int count)
        {
            objectPrefab = prefab;
            initialCount = count;
            InitPool();
        }
        
        protected virtual void InitPool()
        {
            for (int i = 0; i < initialCount; i++)
            {
                T obj = Instantiate(objectPrefab, transform);
                poolQueue.Enqueue(obj);
                obj.gameObject.SetActive(false);

                if (obj is IPoolObject<T>)
                {
                    obj.GetComponent<IPoolObject<T>>().Pool = this;
                }
            }
        }

        public T Get()
        {
            T obj = poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
            poolQueue.Enqueue(obj);

            return obj;
        }

        public T Get(Vector3 position)
        {
            T obj = poolQueue.Dequeue();
            obj.transform.position = position;
            obj.gameObject.SetActive(true);
            poolQueue.Enqueue(obj);

            return obj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }
}