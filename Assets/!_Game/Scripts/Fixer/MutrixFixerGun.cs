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
        private Vector3 m_LastConnectionMiddlePoint;
        private Ray m_LastFireRay;

        private bool m_LeftGun;

        private ParticleSystem m_HitParticleToUse;
        private ParticleSystem m_ConnectingParticleToUse;
        private ParticleSystem m_AttentionParticleToUse;
        private ParticleSystem m_BigConnectionParticleToUse;

        void Awake() {
            m_Camera = GetComponent<Camera>();
            m_LineRenderer = gameObject.AddComponent<LineRenderer>();
            m_FixerData = m_MutrixFixer.GetFixerGunData();

            m_LineRenderer.SetWidth(m_FixerData.laserWidthHit, m_FixerData.laserWidthHit);
        }


        public void Setup(bool isLeftGun) {
            m_LeftGun = isLeftGun;
            m_BigConnectionParticleToUse = m_FixerData.BigConnectingParticles;

            if (m_LeftGun) {
                m_HitParticleToUse = m_FixerData.hitParticlesLeft;
                m_ConnectingParticleToUse = m_FixerData.connectingParticlesLeft;
                m_AttentionParticleToUse = m_FixerData.attentionParticlesLeft;
            }
            else {
                m_HitParticleToUse = m_FixerData.hitParticlesRight;
                m_ConnectingParticleToUse = m_FixerData.connectingParticlesRight;
                m_AttentionParticleToUse = m_FixerData.attentionParticlesRight;
            }
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
            
                m_LastConnectionMiddlePoint = hit.transform.position;
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
            m_HitParticleToUse.gameObject.SetActive(false);
            m_ConnectingParticleToUse.gameObject.SetActive(false);
            m_AttentionParticleToUse.gameObject.SetActive(false);
            m_BigConnectionParticleToUse.gameObject.SetActive(false);

        }

        public void ShootHit() {
            m_LineRenderer.material = m_FixerData.hitLaser;
            m_LineRenderer.SetPosition(0, m_FixerLaserOrigin.position);
            m_LineRenderer.SetPosition(1, m_LastFireHitPoint);
            m_LineRenderer.enabled = true;
            m_HitParticleToUse.transform.position = m_LastFireHitPoint;
            m_HitParticleToUse.gameObject.SetActive(true);
            m_AttentionParticleToUse.transform.position = m_LastFireHitPoint;
            m_AttentionParticleToUse.gameObject.SetActive(true);
        }

        public void ShootMiss() {
            //TODO Fizzle
            m_LineRenderer.material = m_FixerData.hitLaser;
            m_LineRenderer.SetPosition(0, m_FixerLaserOrigin.position);
            m_LineRenderer.SetPosition(1, m_LastFireRay.origin + (m_LastFireRay.direction * 10f));
            m_LineRenderer.enabled = true;
        }

        public void ShootAttention() {
            //Same as hitlaser but different particle
            m_LineRenderer.material = m_FixerData.hitLaser;
            m_LineRenderer.SetPosition(0, m_FixerLaserOrigin.position);
            m_LineRenderer.SetPosition(1, m_LastFireHitPoint);
            m_LineRenderer.enabled = true;
            m_AttentionParticleToUse.transform.position = m_LastFireHitPoint;
            m_AttentionParticleToUse.gameObject.SetActive(true);
        }

        public void ShootConnected() {
            m_LineRenderer.material = m_FixerData.connectedLaser;
            m_LineRenderer.SetPosition(0, m_FixerLaserOrigin.position);
            m_LineRenderer.SetPosition(1, m_LastFireHitPoint);
            m_ConnectingParticleToUse.transform.position = m_LastFireHitPoint;
            m_ConnectingParticleToUse.gameObject.SetActive(true);
            m_BigConnectionParticleToUse.transform.position = m_LastConnectionMiddlePoint;
            m_BigConnectionParticleToUse.gameObject.SetActive(true);



            m_LineRenderer.enabled = true;

        }

    }
}
