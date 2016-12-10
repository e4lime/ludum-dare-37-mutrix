using UnityEngine;

namespace Lime.PhysicsUtility {
    public class LimitRotation : MonoBehaviour {
	
        [SerializeField, Range(0, 180)]
        private float minAngleOffset = 30;
        [SerializeField, Range(0, 180)]
        private float maxAngleOffset = 30;

        [SerializeField]
        private Axis limitRotationOnAxis;

        /// <summary>
        /// Based on given axis and offset
        /// </summary>
        private float m_MinAxisAngle;
        private float m_MaxAxisAngle;

        #region cache
        private Rigidbody m_Rigidbody;
		#endregion

        void Awake(){
            m_Rigidbody = GetComponent<Rigidbody>();
            if (m_Rigidbody) {
                Vector3 currentAngles = m_Rigidbody.rotation.eulerAngles;
                float originAngle = 0;
                switch (limitRotationOnAxis) {
                    case Axis.X:
                        originAngle = currentAngles.x;
                        break;
                    case Axis.Y:
                        originAngle = currentAngles.y;
                        break;
                    case Axis.Z:
                        originAngle = currentAngles.z;
                        break;
                }
                m_MinAxisAngle = originAngle - minAngleOffset;
                m_MaxAxisAngle = originAngle + maxAngleOffset;
               
            }
            else {
                Debug.LogError("No rigidbody on " + gameObject.name, this);
            }
        }

        void FixedUpdate(){
            ClampRotationIfNeeded();
        }

        private void ClampRotationIfNeeded() {
         
            Vector3 currentAngles = m_Rigidbody.rotation.eulerAngles;
     
            switch (limitRotationOnAxis) {
                case Axis.X:
                    currentAngles.x = ClampAngle(currentAngles.x);
                    break;
                case Axis.Y:
                    currentAngles.y = ClampAngle(currentAngles.y);
                    break;
                case Axis.Z:
                    currentAngles.z = ClampAngle(currentAngles.z);
                    break;
            }
  
            m_Rigidbody.rotation = Quaternion.Euler(currentAngles);
        }

        /// <summary>
        /// Takes care of cases when angle is around 360 or 0
        /// From http://answers.unity3d.com/questions/141775/limit-local-rotation.html
        /// </summary>
        private float ClampAngle(float angle) {

            angle = NormalizeAngle(angle);
            if (angle > 180) {
                angle -= 360;
            }
            else if (angle < -180) {
                angle += 360;
            }

            m_MinAxisAngle = NormalizeAngle(m_MinAxisAngle);
            if (m_MinAxisAngle > 180) {
                m_MinAxisAngle -= 360;
            }
            else if (m_MinAxisAngle < -180) {
                m_MinAxisAngle += 360;
            }

            m_MaxAxisAngle = NormalizeAngle(m_MaxAxisAngle);
            if (m_MaxAxisAngle > 180) {
                m_MaxAxisAngle -= 360;
            }
            else if (m_MaxAxisAngle < -180) {
                m_MaxAxisAngle += 360;
            }

            return Mathf.Clamp(angle, m_MinAxisAngle, m_MaxAxisAngle);
        }

       
  
        protected float NormalizeAngle(float angle) {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }
        public enum Axis {
            X,
            Y,
            Z
        }
    }
}
