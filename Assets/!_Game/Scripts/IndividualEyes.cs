﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace Mutrix {

    /// <summary>
    /// Status: Use it! Its weird but more fun than the other controllers
    /// 
    /// Press and hold mouse left to control left eye
    /// Press and hold mouse right to control right eye
    /// Release both to control head
    /// 
    /// Eyes are dependent on head rotations so it gets buggy at certain angles.
    ///     Locked x rotation on head a bit so it won't risk getting buggy
    ///     Potential fix: Edit MouseLook so it uses world rotation?
    /// </summary>
    public class IndividualEyes : MonoBehaviour {
        [Header("Control eyes simultaneously when both buttons are clicked"), SerializeField]
        private bool m_ControlBothEyes = false;
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
        private Camera m_LeftBackgroundCamera;
        [SerializeField]
        private Camera m_RightBackgroundCamera;

        [SerializeField]
        private Camera m_LeftOnlyBoth;
        [SerializeField]
        private Camera m_RightOnlyBoth;

        [SerializeField]
        private MouseLook m_MouseLookLeft;

        [SerializeField]
        private MouseLook m_MouseLookRight;

        [Header("Lock v-rotation so it wont bug"), SerializeField]
        private MouseLook m_MouseLookHead;



        private bool m_Fire1IsDown = false;
        private bool m_Fire2IsDown = false;

        public void Awake() {
            CropCameras();

            m_MouseLookLeft.Init(m_LeftCameraSocket, m_LeftCamera.transform);
            m_MouseLookRight.Init(m_RightCameraSocket, m_RightCamera.transform);
            m_MouseLookHead.Init(transform, m_Head);
        }

        public MouseLook GetMouseLookLeft() {
            return m_MouseLookLeft;
        }

        public MouseLook GetMouseLookRight() {
            return m_MouseLookRight;
        }

        private void CropCameras() {
            Rect leftRect = new Rect(0, 0, 0.5f, 1);
            Rect rightRect = new Rect(0.5f, 0, 0.5f, 1);

            CameraUtility.SetScissorRect(m_LeftCamera, leftRect);
            CameraUtility.SetScissorRect(m_RightCamera, rightRect);
            CameraUtility.SetScissorRect(m_LeftBackgroundCamera, leftRect); 
            CameraUtility.SetScissorRect(m_RightBackgroundCamera, rightRect);
            CameraUtility.SetScissorRect(m_LeftOnlyBoth, leftRect);
            CameraUtility.SetScissorRect(m_RightOnlyBoth, rightRect);

        }

        private void RotateView() {
            if (m_ControlBothEyes) {

                if (m_Fire1IsDown) {
                    m_MouseLookLeft.LookRotation(m_LeftCameraSocket, m_LeftCamera.transform);
                }
                if (m_Fire2IsDown) {
                    m_MouseLookRight.LookRotation(m_RightCameraSocket, m_RightCamera.transform);
                }
            }
            else {
                if (m_Fire1IsDown) {
                    m_MouseLookLeft.LookRotation(m_LeftCameraSocket, m_LeftCamera.transform);
                }
                else if (m_Fire2IsDown) {
                    m_MouseLookRight.LookRotation(m_RightCameraSocket, m_RightCamera.transform);
                }
            }

            if (!m_Fire1IsDown && !m_Fire2IsDown){
                m_MouseLookHead.LookRotation(transform, m_Head);
            }

            
            
        }

        private void ReadInput() {
      
            if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
              
                m_Fire1IsDown = true;
            }
            if (CrossPlatformInputManager.GetButtonDown("Fire2")) {
             
      
                m_Fire2IsDown = true;
            }


            if (CrossPlatformInputManager.GetButtonUp("Fire1")) {
                m_Fire1IsDown = false;
            }
            if (CrossPlatformInputManager.GetButtonUp("Fire2")) {
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
