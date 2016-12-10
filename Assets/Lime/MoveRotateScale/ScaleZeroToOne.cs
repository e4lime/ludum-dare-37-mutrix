using UnityEngine;
using System.Collections;
namespace Lime.MoveRotateScale {
    public class ScaleZeroToOne : MonoBehaviour {

        [SerializeField]
        private Vector3 m_StartingScale = Vector3.zero;
        [SerializeField]
        private float m_ScaleSpeed = 8f;
        [SerializeField]
        private float m_DelayeBeforeScale = 0.0425f;
        [SerializeField]
        private bool m_DisableScriptOnScaleReached = true;

        private Collider m_Collider;
        private bool m_DoScale = true;
        private float m_TimePassed;
      
        void Awake() {
            //  m_collider = GetComponent<Collider>();

            // Builds on android seems to bug out when scaling very small colliders.
            // I enable the collider after a certain size
            //  m_collider.enabled = false;

            this.transform.localScale = m_StartingScale;
        }


        /// <summary>
        /// Move to coroutine!
        /// </summary>
        void Update() {
            m_TimePassed += Time.deltaTime;
            if (m_DoScale && m_TimePassed > m_DelayeBeforeScale) {
                m_DoScale = !Scale();

                // // Builds on android seems to bug out when scaling very small colliders
                //  if (!m_collider.enabled && transform.localScale.sqrMagnitude > 0.03) {
                //   m_collider.enabled = true;
                //  }

                if (!m_DoScale && m_DisableScriptOnScaleReached) {

                    this.enabled = false;
                }
            }
        }

        /// <summary>
        /// Scales transform up to Vector3.one
        /// </summary>
        /// <returns>true if scale reaches vector3.one </returns>
        private bool Scale() {
            Vector3 updatedLocalScale = this.transform.localScale;
            float scaleCompareTreshhold = 0.1f;
            updatedLocalScale = Vector3.Slerp(updatedLocalScale, Vector3.one, Time.deltaTime * m_ScaleSpeed);

            bool targetScaleReached = ((Vector3.one.sqrMagnitude - updatedLocalScale.sqrMagnitude) < scaleCompareTreshhold);
            if (targetScaleReached) {
                updatedLocalScale = Vector3.one;
            }
            this.transform.localScale = updatedLocalScale;
            return targetScaleReached;
        }
    }
}