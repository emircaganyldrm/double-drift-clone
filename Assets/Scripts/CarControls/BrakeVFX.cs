using System;
using UnityEngine;

namespace Car
{
    [RequireComponent(typeof(CarController))]
    public class BrakeVFX : MonoBehaviour
    {
        [SerializeField] private Renderer lightsRenderer;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color brakeColor;
        
        private CarController _carController;
        
        private MaterialPropertyBlock _materialPropertyBlock;

        private bool isBraking;
        
        private const string MainColor = "_Color";
        
        private void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            _carController = GetComponent<CarController>();
        }

        private void OnEnable()
        {
            _carController.OnGas += OnGas;
            _carController.OnBrake += OnBrake;
        }
        
        private void OnDisable()
        {
            _carController.OnGas -= OnGas;
            _carController.OnBrake -= OnBrake;
        }

        private void OnBrake()
        {
            SetBrakeLights();
            isBraking = true;
        }

        private void OnGas()
        {
            SetDefaultColor();
            isBraking = false;
        }


        private void SetBrakeLights()
        {
            if (isBraking)
            {
                return;
            }
            
            lightsRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor(MainColor, brakeColor);
            lightsRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
        
        private void SetDefaultColor()
        {
            if (!isBraking)
            {
                return;
            }
            
            lightsRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor(MainColor, defaultColor);
            lightsRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}