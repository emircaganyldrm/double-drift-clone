using Pools;
using UnityEngine;

namespace Car
{
    public class TrafficCarController : MonoBehaviour, IPoolObject<TrafficCarController>
    {
        public PoolBase<TrafficCarController> Pool { get; set; }

        [SerializeField] private float speed;
        [SerializeField] private Transform[] wheels;
        [SerializeField] private float crashCheckDistance = 10;

        private Transform _parent;
        
        private void FixedUpdate()
        {
            RotateWheels();
            Move();
        }

        private void Move()
        {
            bool raycast =
                Physics.Raycast(transform.position + transform.forward * crashCheckDistance / 4 + transform.up,
                    Vector3.forward, out RaycastHit hit, crashCheckDistance);

            if (!raycast) transform.position += Vector3.forward * (speed * Time.deltaTime);
        }

        private void RotateWheels()
        {
            foreach (Transform wheel in wheels) wheel.Rotate(Vector3.right, 10f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + transform.forward * crashCheckDistance / 2 + transform.up,
                Vector3.forward * crashCheckDistance);
        }

    }
}