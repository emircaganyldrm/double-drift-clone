using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace Pools
{
    public class TilePoolGenerator : MonoBehaviour
    {
        [SerializeField] private Tile[] tiles;
        [SerializeField] private int objectCountPerPool = 50;
        
        private List<TilePool> pools = new List<TilePool>();
        
        public void SpawnPools()
        {
            foreach (var tile in tiles)
            {
                var pool = new GameObject(tile.name + " Pool").AddComponent<TilePool>();
                pool.GeneratePool(tile, objectCountPerPool);
                pool.transform.SetParent(transform);
                pools.Add(pool);
            }    
        }
        
        public Tile GetRandomTile()
        {
            return pools[Random.Range(0, pools.Count)].Get();
        }
    }
}