using UnityEngine;

namespace Managers
{
    public class GameFollowManager : MonoBehaviour
    {
        public static GameFollowManager Instance { get; private set; }
        public Transform FollowTarget { get; private set; }
        
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

        private void Start()
        {
            FollowTarget = PlayerManager.Instance.transform;
        }
        
        public void ChangeTarget(Transform target)
        {
            FollowTarget = target;
        }
    }
}