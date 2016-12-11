using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace Mutrix {

    /// <summary>
    /// Status: Ditch it
    /// 
    /// Press and hold mouse left to control left eye
    /// Press and hold mouse right to control right eye
    /// Release both to control head
    /// 
    /// Eyes are dependent on head rotations so it gets buggy at certain angles
    /// </summary>
    public class IndividualEyes : MonoBehaviour {

        [Header("Parent of eyes (cameras)"), SerializeField]
        private Transform m_Head;
        [Header("Eyes"), SerializeField]
        private Camera m_LeftCamera;
        [SerializeField]
        private Camera m_RightCamera;
        [SerializeField]
        private Transform m_LeftCameraSocket;
        [SerializeField]
        private Transform m_RightCameraSocket;

        [SerializeField]
        private MouseLook m_MouseLookLeft;

        [SerializeField]
        private MouseLook m_MouseLookRight;

        [SerializeField]
        private MouseLook m_MouseLookHead;

        private bool m_Fire1IsDown = false;
        private bool m_Fire2IsDown = false;

        public void Awake() {
            CropCameras();

            m_MouseLookLeft.Init(m_LeftCameraSocket, m_LeftCamera.transform);
            m_MouseLookRight.Init(m_RightCameraSocket, m_RightCamera.transform);
            m_MouseLookHead.Init(transform, m_Head);
        }

        private void CropCameras() {
            Rect leftRect = new Rect(0, 0, 0.5f, 1);
            Rect rightRect = new Rect(0.5f, 0, 0.5f, 1);

            CameraUtility.SetScissorRect(m_LeftCamera, leftRect);
            CameraUtility.SetScissorRect(m_RightCamera, rightRect);
        }

        private void RotateView() {
            if (m_Fire1IsDown) {
                m_MouseLookLeft.LookRotation(m_LeftCameraSocket, m_LeftCamera.transform);
            }
            else if (m_Fire2IsDown) {
                m_MouseLookRight.LookRotation(m_RightCameraSocket, m_RightCamera.transform);
            }
            else {
                m_MouseLookHead.LookRotation(transform, m_Head);
            }

            
            
        }

        private void ReadInput() {
      
            if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
              
                m_Fire1IsDown = true;
            }
            else if (CrossPlatformInputManager.GetButtonDown("Fire2")) {
             
      
                m_Fire2IsDown = true;
            }


            if (CrossPlatformInputManager.GetButtonUp("Fire1")) {
                m_Fire1IsDown = false;
            }
            else if (CrossPlatformInputManager.GetButtonUp("Fire2")) {
                m_Fire2IsDown = false;
            }

        }

        public void Update() {
            ReadInput();
            RotateView();
        }

        public void FixedUpdate() {
            m_MouseLookLeft.UpdateCursorLock();
            m_MouseLookRight.UpdateCursorLock();
        }
    }
}
