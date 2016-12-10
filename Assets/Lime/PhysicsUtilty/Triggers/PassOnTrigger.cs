using UnityEngine;
namespace Lime.PhysicsUtility.Triggers {
    public class PassOnTrigger : MonoBehaviour {

        [SerializeField]
        private GameObject m_Target;

        private IOnTriggerEnterReceiver[] m_AllOnTriggerEnter;
        private IOnTriggerExitReceiver[] m_AllOnTriggerExit;


        public GameObject Target
        {
            set
            {
                m_Target = value;
            }
            get
            {
                return m_Target;
            }
        }

        void Start() {

            m_AllOnTriggerEnter = m_Target.GetComponents<IOnTriggerEnterReceiver>();
            m_AllOnTriggerExit = m_Target.GetComponents<IOnTriggerExitReceiver>();
        }

        void OnTriggerEnter(Collider other) {

            if (m_AllOnTriggerEnter != null) {
                foreach (IOnTriggerEnterReceiver receiver in m_AllOnTriggerEnter) {
                    receiver.OnTriggerEnter(other);
                }
            }
        }

        void OnTriggerExit(Collider other) {
            if (m_AllOnTriggerExit != null) {
                foreach (IOnTriggerExitReceiver receiver in m_AllOnTriggerExit) {
                    receiver.OnTriggerExit(other);
                }
            }
        }
    }

}
