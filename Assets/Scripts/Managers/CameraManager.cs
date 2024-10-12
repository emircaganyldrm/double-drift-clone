using System.Collections.Generic;
using UnityEngine;

namespace CameraControls
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;

        [SerializeField] private CameraID startCamera;
        [SerializeField] private CameraData[] cameras;
    
        private CameraID _currentCamera;
    
        private Dictionary<CameraID, Camera> _cameraDictionary = new Dictionary<CameraID, Camera>();
    
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
        
            CreateDictionary();
        }

        private void Start()
        {
            ChangeCamera(startCamera);
        }

        public void ChangeCamera(CameraID cameraID)
        {
            GetCamera(_currentCamera)?.gameObject.SetActive(false);
            _currentCamera = cameraID;
            GetCamera(_currentCamera).gameObject.SetActive(true);
        }
    
        private Camera GetCamera(CameraID cameraID)
        {
            return _cameraDictionary[cameraID];
        }
    
        private void CreateDictionary()
        {
            foreach (CameraData cameraData in cameras)
            {
                _cameraDictionary.Add(cameraData.cameraID, cameraData.camera);
            }
        }
    }
}

