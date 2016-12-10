using UnityEngine;
using System.Collections;

/// <summary>
/// UPDATE: If I want to use this then rethink my design. Maybe the collision detection should be on the other object.
/// 
/// Passes OnCollision events to other methods.
/// Workes well with LimeLimitOnCollisionEnter, for example if we dont have a compound collider we can use this class instead.
/// </summary>
namespace Lime.PhysicsUtility.Collisions {
    public class LimeOnCollisionHandler : MonoBehaviour, IOnCollisionEnterReceiver {

    
        public delegate void OnCollisionListener(Collision other);

        [SerializeField]
        public OnCollisionListener m_OnCollisionEnterListeners;

        [SerializeField]
        public OnCollisionListener m_OnCollisionExitListeners;

        [SerializeField]
        public OnCollisionListener m_OnCollisionStayListeners;



        void OnCollisionEnter(Collision other) {
            if (m_OnCollisionEnterListeners != null) {
               m_OnCollisionEnterListeners(other);
            }
        }

        void OnCollisionExit(Collision other) {
            if (m_OnCollisionExitListeners != null) {
                m_OnCollisionExitListeners(other);
            }
        }


        void OnCollisionStay(Collision other) {
            if (m_OnCollisionStayListeners != null) {
                m_OnCollisionStayListeners(other);
            }
        }

  
        /// <summary>
        /// For those cases when the RB isnt on this.gameObject
        /// </summary>
        /// <param name="collision"></param>
        void IOnCollisionEnterReceiver.OnCollisionEnter(Collision collision) {
            OnCollisionEnter(collision);
        }
    }
}
