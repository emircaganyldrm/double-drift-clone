using UnityEngine;

namespace Pools
{
    public interface IPoolObject<T> where T : MonoBehaviour
    {
        PoolBase<T> Pool { get; set; }
    }
}