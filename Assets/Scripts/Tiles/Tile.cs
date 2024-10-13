using Pools;
using UnityEngine;

namespace Tiles
{
    public class Tile : MonoBehaviour, IPoolObject<Tile>
    {
        public PoolBase<Tile> Pool { get; set; }

        public virtual void OnInitialized()
        {
            
        }
    }
}