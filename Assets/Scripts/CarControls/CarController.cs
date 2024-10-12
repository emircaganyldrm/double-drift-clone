using InputControls;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        //Movement
        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private AnimationCurve torqueCurve;
        [SerializeField] private float torqueMultiplier = 50;
        [SerializeField] private float topSpeed = 70;
        [SerializeField] private float horizontalSpeed = 7;
        [SerializeField] private Vector2 bounds = new(-3, 3);
        
        //Rotation
        [SerializeField] private Transform frontWheels;
        [SerializeField] private Transform rearWheels;
        [SerializeField] private float wheelMaxAngle = 45;
        [SerializeField] private float wheelTurnSpeed = 900;
        

        private float _currentSpeed;
        private float _currentTorque;

        private Vector3 _velocity;

        public float SpeedProgress => _currentSpeed / topSpeed;
        public bool IsDrifting { get; private set; }
        

        private void Update()
        {
            HandleSpeed();
            CalculateTorque();
        }

        private void FixedUpdate()
        {
            MoveCar();
            HandleRotation();
            HandleWheels();
        }

        private void HandleSpeed()
        {
            float input = InputManager.Instance.IsTapping ? 1 : -1;
            _currentSpeed += accelerationCurve.Evaluate(_currentSpeed / topSpeed) * input * _currentTorque;
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
            _currentTorque = torqueCurve.Evaluate(_currentSpeed / topSpeed) * torqueMultiplier * Time.deltaTime;
        }

        private void HandleRotation()
        {
            float direction = Mathf.Sign(transform.position.x);

            bool input = InputManager.Instance.IsTapping;
            
            float rotation = transform.position.x / bounds.y * direction;

            if (input)
            {
                Quaternion targetRotation = Quaternion.Euler(Vector3.up * (-70 * direction));
                transform.rotation = Quaternion.Lerp(Quaternion.identity, targetRotation,
                    rotation * Time.deltaTime * 60);
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(Vector3.up * 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                    Mathf.Abs(rotation) * Time.deltaTime * 10);
            }

            IsDrifting = Mathf.Abs(transform.localEulerAngles.y) > 20;
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
                child.eulerAngles = fwRotation;
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