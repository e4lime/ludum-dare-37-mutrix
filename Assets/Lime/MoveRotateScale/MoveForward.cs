using UnityEngine;

namespace Lime.MoveRotateScale {

    /// <summary>
    /// Best on kinematic 
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MoveForward : MonoBehaviour {


        [SerializeField, Range(-1, 1)]
        private float m_Forward = 1f;
        [SerializeField] private float m_MaxSpeed = 5f;
        [SerializeField] private bool m_MoveLocalForward = true;
        [SerializeField] private Vector3 m_CustomForward;



 

        private Rigidbody m_Rigidbody;
        private Transform m_Transform;
    

        void Awake() {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Transform = transform;
        }

        public float GetForward() {
            return m_Forward;
        }

        public float MaxSpeed
        {
            get
            {
                return m_MaxSpeed;
            }
            set
            {
                m_MaxSpeed = value;
            }
        }

        void FixedUpdate() {
            Vector3 forward = m_CustomForward;
            if (m_MoveLocalForward) {
                forward = m_Transform.forward;
            }
            m_Rigidbody.MovePosition(m_Transform.position + (forward * m_MaxSpeed * m_Forward) * Time.fixedDeltaTime);
        }
    }
}
