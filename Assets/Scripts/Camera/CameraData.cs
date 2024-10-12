using System;
using UnityEngine;

namespace CameraControls
{
    [Serializable]
    public struct CameraData
    {
        public CameraID cameraID;
        public Camera camera;
    }
}