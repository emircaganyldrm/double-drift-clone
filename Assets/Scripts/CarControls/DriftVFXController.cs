using System;
using UnityEngine;

namespace Car
{
    [RequireComponent(typeof(CarController))]
    public class DriftVFXController : MonoBehaviour
    {
        private CarController _carController;
        private DriftVFX[] _driftVFXs;
        
        private void Awake()
        {
            _driftVFXs = GetComponentsInChildren<DriftVFX>();
            _carController = GetComponent<CarController>();
        }

        private void Update()
        {
            if (_carController.IsDrifting)
            {
                PlayVFX();
            }
            else
            {
                StopVFX();
            }
        }

        private void PlayVFX()
        {
            foreach (var driftVFX in _driftVFXs)
            {
                driftVFX.Play();
            }
        }
        
        private void StopVFX()
        {
            foreach (var driftVFX in _driftVFXs)
            {
                driftVFX.Stop();
            }
        }
    }
}