using Pools;
using UnityEngine;

namespace Utils
{
    public class RandomObjectPool<T> : PoolBase<T> where T : MonoBehaviour
    {
        [SerializeField] private T[] options;

        public void GeneratePool(T[] options)
        {
            this.options = options;
            InitPool();
        }
        
        protected override void InitPool()
        {
            for (int i = 0; i < initialCount; i++)
            {
                T obj = Instantiate(options[Random.Range(0, options.Length)], transform);
                poolQueue.Enqueue(obj);
                obj.gameObject.SetActive(false);

                if (obj is IPoolObject<T>)
                {
                    obj.GetComponent<IPoolObject<T>>().Pool = this;
                }
            }
        }
    }
}