using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace Mutrix {

    /// <summary>
    /// Status: Might use
    /// Control right camera and have left camera as child to right
    /// 
    /// </summary>
    public class Eyes : MonoBehaviour {

        [Header("Parent of eyes (cameras)"), SerializeField]
        private Transform m_Head;
        [Header("Eyes"), SerializeField]
        private Camera m_LeftCamera;
        [SerializeField]
        private Camera m_RightCamera;
     

        public void Awake() {
            CropCameras();

        }

        private void CropCameras() {
            Rect leftRect = new Rect(0, 0, 0.5f, 1);
            Rect rightRect = new Rect(0.5f, 0, 0.5f, 1);

            Utility.SetScissorRect(m_LeftCamera, leftRect);
            Utility.SetScissorRect(m_RightCamera, rightRect);
        }

    }
}
