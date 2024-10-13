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

        [ColorUsage(true, true)]
        [SerializeField] private Color brakeEmissionColor;
        
        private CarController _carController;
        
        private MaterialPropertyBlock _materialPropertyBlock;

        private bool isBraking;
        
        private const string MainColor = "_Color";
        private const string EmissionColor = "_EmissionColor";
        
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
            _materialPropertyBlock.SetColor(EmissionColor, brakeEmissionColor);
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
            _materialPropertyBlock.SetColor(EmissionColor, Color.black);
            lightsRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}