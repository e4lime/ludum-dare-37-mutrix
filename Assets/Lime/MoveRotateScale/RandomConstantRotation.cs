using UnityEngine;

namespace Lime.MoveRotateScale {
    public class RandomConstantRotation : MonoBehaviour {
        [SerializeField]
        private float m_MinRandomRotation = -200f;
        [SerializeField]
        private float m_MaxRandomRotation = 200f;
        //[SerializeField]
        //private bool m_RandomStartingRotation = false; //if rotation should be randomized at start
        [SerializeField]
        private bool m_StartRotationFromScriptCall = false;

 
        void Start() {
            if (!m_StartRotationFromScriptCall) {
                StartRotating();
            }
        }

        public void StartRotating() {
            if (m_MinRandomRotation != 0f && m_MaxRandomRotation != 0f) {
                LimeAssist.SetRandomRotationConstant(this.transform, m_MinRandomRotation, m_MaxRandomRotation, m_StartRotationFromScriptCall);
            }
        }
    }
}
