using UnityEngine;

namespace Car
{
    [RequireComponent(typeof(CarController), typeof(CarCrashHandler))]
    public class DriftVFXController : MonoBehaviour
    {
        private CarController _carController;
        private CarCrashHandler _carCrashHandler;
        
        private DriftVFX[] _driftVFXs;
        
        private void Awake()
        {
            _driftVFXs = GetComponentsInChildren<DriftVFX>();
            _carController = GetComponent<CarController>();
            _carCrashHandler = GetComponent<CarCrashHandler>();
        }

        private void OnEnable()
        {
            _carCrashHandler.OnCarCrash += KillVFX;
        }
        
        private void OnDisable()
        {
            _carCrashHandler.OnCarCrash -= KillVFX;
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
        
        private void KillVFX()
        {
            // Kill behaviour may be added later on
            StopVFX();
        }
    }
}