using System;
using UnityEngine;

namespace Car
{
    public class CarCrashHandler : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbodyPrefab;
        public event Action OnCarCrash;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TrafficCarController _))
            {
                OnCarCrash?.Invoke();
                Instantiate(rigidbodyPrefab, transform.position, transform.rotation);
                gameObject.SetActive(false);
                Invoke(nameof(EndGame), 2);
            }
        }
        
        private void EndGame()
        {
            GameManager.Instance.EndGame();
        }
    }
}