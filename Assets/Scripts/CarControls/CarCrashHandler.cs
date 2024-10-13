using System;
using UnityEngine;

namespace Car
{
    public class CarCrashHandler : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbodyPrefab;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TrafficCarController _))
            {
                Rigidbody newPrefab = Instantiate(rigidbodyPrefab, transform.position, transform.rotation);
                newPrefab.AddForceAtPosition(new Vector3(0,1,10),transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}