using System;
using UnityEngine;

namespace InputControls
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        
        public float SwipeDelta { get; private set; }
        public bool IsTapping { get; private set; }
        
        private Vector2 _startTouch;
        
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

        private void Update()
        {
            SetSwipeDelta();
            IsTapping = Input.GetMouseButton(0);
        }

        private void SetSwipeDelta()
        {
            SwipeDelta = 0;
            if (Input.GetMouseButtonDown(0))
            {
                _startTouch = Input.mousePosition;
            }
            else
            {
                SwipeDelta = (Input.mousePosition.x - _startTouch.x)/Screen.width;
            }
        }
    }
}