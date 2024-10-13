using System;
using UnityEngine;
using Utils;

namespace Car
{
    public class TrafficCarPool : RandomObjectPool<TrafficCarController>
    {
        public static TrafficCarPool Instance { get; private set; }
        [SerializeField] private TrafficCarLibrary carLibrary;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            GeneratePool(carLibrary.trafficCarControllers);
        }
    }
}