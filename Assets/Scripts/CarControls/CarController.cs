using InputControls;
using UnityEngine;
using UnityEngine.Serialization;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [Header("Speed Settings")] 
        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private AnimationCurve torqueCurve;
        [SerializeField] private AnimationCurve breakCurve;
        [SerializeField] private float topSpeed = 30;
        [SerializeField] private float torqueMultiplier = 50;
        [SerializeField] private float maxTorque = 100;
        [SerializeField] private float breakForce = 2;
        [SerializeField] private float horizontalSpeed = 10;
        [SerializeField] private Vector2 bounds = new(-3, 3);

        [Space] [Header("Drift Settings")]
        [SerializeField] private float driftThreshold = 20;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float rotationMaxAngle = 100f;
        [SerializeField] private float verticalRotation = .04f;
        [SerializeField] private float horizontalRotation = .2f;
        [SerializeField] private float wheelMaxAngle = 30;
        [SerializeField] private float wheelTurnSpeed = 900;

        [Space] [Header("Parts")] 
        [SerializeField] private Transform carFrame;
        [SerializeField] private Transform frontWheels;
        [SerializeField] private Transform rearWheels;

        private float _currentSpeed;
        private float _currentTorque;
        private Vector3 _velocity;
        private float _currentRotation;

        public float SpeedProgress => _currentSpeed / topSpeed;
        public bool IsDrifting { get; private set; }

        private void Update()
        {
            HandleSpeed();
            CalculateTorque();
            
            IsDrifting = Mathf.Abs(_currentRotation) > driftThreshold;
        }

        private void FixedUpdate()
        {
            MoveCar();
            HandleRotation();
            HandleWheels();
        }

        private void HandleSpeed()
        {
            float input = InputManager.Instance.IsTapping ? 1 : 0;
            _currentSpeed += accelerationCurve.Evaluate(SpeedProgress) * input * Time.deltaTime * _currentTorque;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, topSpeed);

            if (input != 0) return;
            _currentSpeed -= breakCurve.Evaluate(SpeedProgress) * breakForce * Time.deltaTime;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, topSpeed);
        }

        private void MoveCar()
        {
            bool input = InputManager.Instance.IsTapping;

            Vector3 forwardMovement = Vector3.forward * (_currentSpeed * Time.deltaTime);

            Vector3 horizontalMovement = Vector3.zero;
            if (input)
                horizontalMovement =
                    Vector3.right * (InputManager.Instance.SwipeDelta * horizontalSpeed * Time.deltaTime);

            _velocity = forwardMovement + horizontalMovement;

            Vector3 desiredPosition = transform.position + _velocity;
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, bounds.x, bounds.y);

            transform.position = desiredPosition;
        }

        private void CalculateTorque()
        {
            float input = InputManager.Instance.IsTapping ? 1 : -1;
            _currentTorque += torqueCurve.Evaluate(_currentSpeed / topSpeed) * input * Time.deltaTime *
                              torqueMultiplier;
            _currentTorque = Mathf.Clamp(_currentTorque, 0, maxTorque);
        }

        private void HandleRotation()
        {
            bool input = InputManager.Instance.IsTapping;

            if (input)
            {
                float swipeDelta = InputManager.Instance.SwipeDelta;
                float targetRotationAngle = swipeDelta * -rotationMaxAngle;
                _currentRotation =
                    Mathf.Lerp(_currentRotation, targetRotationAngle, Time.deltaTime * rotationSpeed);
            }
            else
            {
                _currentRotation = Mathf.Lerp(_currentRotation, 0, Time.deltaTime * rotationSpeed / 3);
            }

            Quaternion targetRotation = Quaternion.Euler(0, _currentRotation, 0);
            transform.rotation = targetRotation;

            carFrame.localRotation = Quaternion.Lerp(carFrame.localRotation,
                Quaternion.Euler(-_currentTorque * verticalRotation, 0, _currentRotation * horizontalRotation),
                Time.deltaTime * 10);
        }

        private void HandleWheels()
        {
            float spin = SpeedProgress * Time.deltaTime * wheelTurnSpeed;

            for (int i = 0; i < frontWheels.childCount; i++)
            {
                Transform child = frontWheels.GetChild(i);
                child.Rotate(Vector3.right * spin);

                Vector3 fwRotation = child.eulerAngles;
                fwRotation.y = Mathf.Clamp(InputManager.Instance.SwipeDelta * wheelMaxAngle, -wheelMaxAngle,
                    wheelMaxAngle);
                child.eulerAngles = -fwRotation;
            }

            rearWheels.Rotate(Vector3.right * spin);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(bounds.x, 1, 0), new Vector3(bounds.x, 1, 10));
            Gizmos.DrawLine(new Vector3(bounds.y, 1, 0), new Vector3(bounds.y, 1, 10));
        }
    }
}