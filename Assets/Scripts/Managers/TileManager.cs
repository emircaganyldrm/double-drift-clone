using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Pools;
using UnityEngine;

namespace Tiles
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private TilePoolGenerator tilePoolGenerator;
        [SerializeField] private int tileIntervalToReplace = 3;
        [SerializeField] private float zOffset = 120f;
        [SerializeField] private int countToPlace;
        
        private List<Tile> _tiles = new List<Tile>();
        
        private int _currentTileIndex = 1;
        
        private IEnumerator Start()
        {
            tilePoolGenerator.SpawnPools();

            yield return new WaitForEndOfFrame();
            
            for (int i = 0; i < countToPlace; i++)
            {
                SpawnTileSet();
            }
        }

        private void Update()
        {
            if (_tiles.Count > 1)
            {
                float distance = Vector3.Distance(_tiles[0].transform.position, GameFollowManager.Instance.FollowTarget.position);
                if (distance > zOffset * tileIntervalToReplace )
                {
                    PlaceNextTile();
                }
            }
        }

        [ContextMenu("Get Next Tile")]
        private void PlaceNextTile()
        {
            if (_tiles.Count > 0)
            {
                var tile = _tiles[0];
                _tiles.RemoveAt(0);
                tile.Pool.Return(tile);
            }
            
            SpawnTileSet();
        }
        
        private void SpawnTileSet()
        {
            Tile tile = tilePoolGenerator.GetRandomTile();
            tile.transform.position = new Vector3(0, 0, _currentTileIndex * zOffset);
            _tiles.Add(tile);
            _currentTileIndex++;
            tile.OnInitialized();
        }
    }
}