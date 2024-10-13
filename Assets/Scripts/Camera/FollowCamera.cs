using Managers;
using UnityEngine;

namespace CameraControls
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private Vector3 offset;

        private void FixedUpdate()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            transform.position = Vector3.Lerp(transform.position, GameFollowManager.Instance.FollowTarget.position + offset, speed * Time.deltaTime);
        }

        private void OnValidate()
        {
            transform.localPosition = offset;
        }
    }
}

