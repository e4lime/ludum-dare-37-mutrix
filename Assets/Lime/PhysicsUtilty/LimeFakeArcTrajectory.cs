using UnityEngine;
namespace Lime.PhysicsUtility {
    [RequireComponent(typeof(Rigidbody))]
    public class LimeFakeArcTrajectory : MonoBehaviour {
        private Rigidbody m_Rigidbody;
        private Vector3 m_Origin;
        private Vector3 m_Target;
        private float m_AirTime;
        private bool m_OptionsSet = false;
        private bool m_Launch = false;

        private float m_TimeLeft;

        public void Start() {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Origin = m_Rigidbody.transform.position;
            m_Rigidbody.isKinematic = true;
        }

        public void SetLaunchOptions(Vector3 target, float airTime) {
            m_Target = target;
            m_AirTime = airTime;
            m_AirTime = 0.3f;
            m_TimeLeft = m_AirTime;
            m_OptionsSet = true;
        }

        public void Launch() {
            if (!m_OptionsSet) {
                Debug.LogError("Launchoptions not set", this);
                return;
            }
            m_Launch = true;
        }

        public void FixedUpdate() {
            if (m_Launch) {
                MoveTowardsTarget();
            }

        }

        private void MoveTowardsTarget() {
            
            m_TimeLeft -= Time.fixedDeltaTime;
            Debug.Log(m_Origin);
            float progress = Mathf.InverseLerp(m_AirTime, 0f, m_TimeLeft);
            Vector3 newPosition = Vector3.Lerp(m_Origin, m_Target, progress);
            newPosition.y = Mathf.Cos(Mathf.Lerp(-Mathf.PI * 0.5f, Mathf.PI * 0.5f, progress));
            this.transform.position = newPosition;
            if (m_TimeLeft < 0) {
                m_Launch = false;
            }
        }

    }
}
