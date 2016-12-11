using UnityEngine;
using System.Collections;

namespace Mutrix.Fixer {
    [RequireComponent(typeof(Camera))]
    
    public class MutrixFixerGun : MonoBehaviour {

        [Header("Where Laser shoots from"), SerializeField]
        private Transform m_FixerLaserOrigin;
        [Header("Pulls data from MutrixFixer"), SerializeField]
        private MutrixFixer m_MutrixFixer;

        private MutrixFixer.FixerGunData m_FixerData;
        private Camera m_Camera;
        private LineRenderer m_LineRenderer;

        private Vector3 m_LastFireHitPoint;
        private Ray m_LastFireRay;
      
        
        void Awake() {
            m_Camera = GetComponent<Camera>();
            m_LineRenderer = gameObject.AddComponent<LineRenderer>();
            m_FixerData = m_MutrixFixer.GetFixerGunData();

            m_LineRenderer.SetWidth(m_FixerData.laserWidthHit, m_FixerData.laserWidthHit);
        }

        /// <summary>
        /// Fire with raycast. Returns what it hits or null if nothing.
        /// Updates LastFire
        /// </summary>
        /// <returns>Null if nothing</returns>
        public GameObject Fire() {

            RaycastHit hit;
            Ray ray = m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            this.m_LastFireRay = ray;
            if (Physics.Raycast(ray, out hit, m_FixerData.fireLength)) {
                m_LastFireHitPoint = hit.point;
                return hit.transform.gameObject;
                // GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // s.transform.position = hit.point;
                // Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
            }
            else {
             
                    return null;
                //Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 2f);
            }
        }
        
        public void StopFire() {
            m_LineRenderer.enabled = false;
        }

        public void ShootHit() {
            m_LineRenderer.material = m_FixerData.hitLaser;
            m_LineRenderer.SetPosition(0, m_FixerLaserOrigin.position);
            m_LineRenderer.SetPosition(1, m_LastFireHitPoint);
            m_LineRenderer.enabled = true;
        }

        public void ShootMiss() {
            //TODO Fizzle
            m_LineRenderer.material = m_FixerData.hitLaser;
            m_LineRenderer.SetPosition(0, m_FixerLaserOrigin.position);
            m_LineRenderer.SetPosition(1, m_LastFireRay.origin + (m_LastFireRay.direction * 10f));
            m_LineRenderer.enabled = true;
        }

        public void ShootConnected() {
            m_LineRenderer.material = m_FixerData.connectedLaser;
            m_LineRenderer.SetPosition(0, m_FixerLaserOrigin.position);
            m_LineRenderer.SetPosition(1, m_LastFireHitPoint);
            m_LineRenderer.enabled = true;

        }

    }
}
