using System.Collections.Generic;
using Car;
using Managers;
using UnityEngine;

namespace Tiles
{
    public class TrafficSpawner : Tile
    {
        [SerializeField] private TrafficCarLibrary carLibrary;

        [SerializeField] private float[] lanePositions;

        [SerializeField] private float roadLength = 120;

        private List<TrafficCarController> _cars = new();

        public override void OnInitialized()
        {
            SpawnCars();
        }

        private void SpawnCars()
        {
            var pool = TrafficCarPool.Instance;

            foreach (float lanePosition in lanePositions)
            {
                TrafficCarController car = pool.Get();
                car.transform.position =
                    new Vector3(lanePosition, 0, transform.position.z + Random.Range(0, roadLength));
                _cars.Add(car);
                car.transform.SetParent(null);
            }
        }

        private void Update()
        {
            CheckCarDistances();
        }

        private void CheckCarDistances()
        {
            foreach (TrafficCarController car in _cars)
            {
                float distance = Vector3.Distance(car.transform.position,
                    GameFollowManager.Instance.FollowTarget.position);
                
                float dot = Vector3.Dot(car.transform.forward,
                    GameFollowManager.Instance.FollowTarget.position - car.transform.position);

                if (dot > 0 && distance > 20)
                {
                    car.Pool.Return(car);
                    _cars.Remove(car);
                    break;
                }
            }
        }
    }
}