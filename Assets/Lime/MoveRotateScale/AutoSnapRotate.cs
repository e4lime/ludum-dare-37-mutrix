using UnityEngine;

namespace Lime.MoveRotateScale {
    public class AutoSnapRotate : MonoBehaviour {

        [SerializeField]
        private float m_RotateValue;

        [SerializeField]
        private float m_TimeBetweenRotations;


        private float m_TimePassed;

        void Update() {
            m_TimePassed += Time.deltaTime;
            if (m_TimePassed >= m_TimeBetweenRotations) {
                DoRotate();
                m_TimePassed = 0;
            }

        }

        private void DoRotate() {
            this.transform.RotateAround(this.transform.position, transform.up, m_RotateValue);
        }
    }
}