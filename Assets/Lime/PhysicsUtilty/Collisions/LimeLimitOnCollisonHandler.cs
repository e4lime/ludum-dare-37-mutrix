using UnityEngine;
using System.Collections.Generic;

namespace Lime.PhysicsUtility.Collisions {
    /// <summary>
    /// UPDATE: If I want to use this rethink my design, maybe the other object should do the collision detection?
    /// 
    /// For compound colliders. 
    /// Limits OnCollisionEnter calls to just one. For the special cases where the "other collider" covers two or more parts of the compound collider.
    /// </summary>
    public class LimeLimitOnCollisonHandler: MonoBehaviour, IOnCollisionEnterReceiver {



        /// <summary>
        /// Keep track of collisions with these tags. 
        /// If empty all tags are valid
        /// </summary>
        [SerializeField]
        private string[] m_ValidTags;


        public delegate void OnCollisionEnterLimiter(Collision other);
        public OnCollisionEnterLimiter LimitedOnCollisionEnterListeners;

        /// <summary>
        /// Keeps track of collisions. Doing this cause of compound collider, theres a chance that OnCollisionEnter will trigger
        /// multiple times if the incoming collider hits more than one collider in the compound collider.
        /// 
        /// Wont be used if entity this script is attached to doesn't have a compound collider (see SetIsCompoundCollider())
        /// 
        /// Key: ID of collider hitting Target.
        /// Value: How many times key has hitted target
        /// </summary>
        private Dictionary<Collider, int> m_Collisions;

        void Awake() {
            m_Collisions = new Dictionary<Collider, int>();
        }

        void OnCollisionEnter(Collision other) {

            GameObject otherGo = other.gameObject;
            if (!ValidCollider(otherGo)) {
                return;
            }

            bool passCollision = true;

            Collider key = other.contacts[0].otherCollider;

            int value;
            if (m_Collisions.TryGetValue(key, out value)) {
                m_Collisions[key] = ++value;
            }
            else {
                m_Collisions.Add(key, 1);
            }
            if (m_Collisions[key] > 1) {
                passCollision = false;
            }

            if (passCollision) {
                if (LimitedOnCollisionEnterListeners != null) {
                    LimitedOnCollisionEnterListeners(other);
                }
            }

        }

        void OnCollisionExit(Collision other) {

            GameObject go = other.gameObject;

            if (!ValidCollider(go)) {
                return;
            }
            DecrementCollider(other.collider);
        }

        public void SetTags(string[] tags) {
            m_ValidTags = tags;
        }

        public void DecrementCollider(Collider target) {
            m_Collisions[target]--;
            if (m_Collisions[target] < 0) {
                Debug.LogError("Design error! Less than 0 collisions. Fix the solution!", this);
            }
        }

        private bool ValidCollider(GameObject otherCollider) {
            if (m_ValidTags == null || m_ValidTags.Length == 0) {
                return true;
            }

            bool valid = false;
            foreach (string tag in m_ValidTags) {
                valid = otherCollider.CompareTag(tag);
                if (valid) {
                    break;
                }
            }

            return valid;
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
