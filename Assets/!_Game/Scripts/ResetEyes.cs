using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;

namespace Mutrix.Fixer {
    public class ResetEyes : MonoBehaviour {

        [SerializeField]
        Transform m_LeftSocket;
        [SerializeField]
        Transform m_LeftEye;
        [SerializeField]
        Transform m_RightSocket;
        [SerializeField]
        Transform m_RightEye;


        [SerializeField]
        private float m_TimeRotate = 1;
        [SerializeField]
        private RotateMode m_RotateMode = RotateMode.WorldAxisAdd;

        [SerializeField]
        private Ease m_Ease = Ease.Linear;

        private bool m_ResetEyes;

        
      
        void Update() {
            if (!m_ResetEyes) {
                m_ResetEyes = CrossPlatformInputManager.GetButtonDown("ResetEyes");
            }


            if (m_ResetEyes) {
                StartResetEyes();
            }

        }

        private void StartResetEyes() {

            m_LeftSocket.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);
            m_RightSocket.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);
            m_LeftEye.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);
            m_RightEye.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);

            m_ResetEyes = false;
        }
    }
}