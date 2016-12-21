using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;
using UnityStandardAssets.Characters.FirstPerson;

namespace Mutrix.Fixer {
    public class ResetEyes : MonoBehaviour {

        [SerializeField]
        private Transform m_LeftSocket;
        [SerializeField]
        private Transform m_LeftEye;
        [SerializeField]
        private Transform m_RightSocket;
        [SerializeField]
        private Transform m_RightEye;

       
        private MouseLook m_LeftEyeMouseLook;
        private MouseLook m_RightEyeMouseLook;


        [SerializeField]
        private float m_TimeRotate = 1;
        [SerializeField]
        private RotateMode m_RotateMode = RotateMode.WorldAxisAdd;

        [SerializeField]
        private Ease m_Ease = Ease.Linear;

        private bool m_ResetEyes;

        void Start() {
            IndividualEyes eyes = GetComponent<IndividualEyes>();
            m_LeftEyeMouseLook = eyes.GetMouseLookLeft();
            m_RightEyeMouseLook = eyes.GetMouseLookRight();
        }
      
        void Update() {
            if (!m_ResetEyes) {
                m_ResetEyes = CrossPlatformInputManager.GetButtonDown("ResetEyes");
            }


            if (m_ResetEyes) {
                StartResetEyes();
                m_LeftEyeMouseLook.ResetRotations();
                m_RightEyeMouseLook.ResetRotations();
            }

        }

        private void StartResetEyes() {

            Sequence seq = DOTween.Sequence();
            
            seq.Insert(0, m_LeftSocket.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease));
            seq.Insert(0, m_RightSocket.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease));
            seq.Insert(0, m_LeftEye.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease));
            seq.Insert(0, m_RightEye.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease));
           // seq.AppendCallback();

        //    m_LeftSocket.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);
        //    m_RightSocket.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);
        //    m_LeftEye.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);
        //    m_RightEye.DOLocalRotate(new Vector3(0, 0, 0), m_TimeRotate, m_RotateMode).SetEase(m_Ease);

            m_ResetEyes = false;
        }
    }
}