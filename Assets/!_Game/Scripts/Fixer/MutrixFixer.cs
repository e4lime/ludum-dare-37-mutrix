using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using Mutrix.WorldObjects;

namespace Mutrix.Fixer {


    /// <summary>
    /// Waaay to tired when I did this. The state thing is just annoying.
    /// 
    /// Dependent on FPCTwoEyes state
    /// </summary>
    public class MutrixFixer : MonoBehaviour {

        [SerializeField]
        private MutrixFixerGun m_LeftEye;

        [SerializeField]
        private MutrixFixerGun m_RightEye;

        [SerializeField]
        private FixerGunData m_FixerGunData;

        private FirstPersonControllerTwoEyes m_Controller;

        private bool m_FireFix = false;

        public FixerGunData GetFixerGunData() { return m_FixerGunData; }

        private State m_CurrentState = State.NotShooting;

        public enum State {
            NotShooting,
            Shooting,
            ShootingConnected
        }


        void Start() {
            m_Controller = GetComponent<FirstPersonControllerTwoEyes>();

            m_LeftEye.Setup(true);
            m_RightEye.Setup(false);
        }

        // Update is called once per frame
        void Update() {
            ReadInput();
            if (m_CurrentState == State.NotShooting) {
                if (m_FireFix && m_Controller.IsStandingStill()) {
                    FireGuns();
                    m_FireFix = false;
                }
            }
            else {
                if (!m_Controller.IsStandingStill()) {
                    ChangeState(State.NotShooting);
                }
            }
         

        }

        public State GetState() {
            return m_CurrentState;
        }

        private void ChangeState(State to) {
            if (to == State.NotShooting && m_CurrentState != State.NotShooting) {
                m_LeftEye.StopFire();
                m_RightEye.StopFire();
            }
            else if (m_CurrentState == State.NotShooting && to == State.Shooting) {
                StartCoroutine(StopShootingAfter(m_FixerGunData.laserTimeHit));
            }
            else if(m_CurrentState == State.NotShooting && to == State.ShootingConnected) {
                StartCoroutine(StopShootingAfter(m_FixerGunData.laserTimeConnect));
            }
            m_CurrentState = to;
        }
        
        private void FireGuns() {
            GameObject targetLeft = m_LeftEye.Fire();
            GameObject targetRight = m_RightEye.Fire();

            // Same target?
            if ((targetLeft != null && targetRight != null) && targetLeft == targetRight) {
                WorldObject left = targetLeft.GetComponent<WorldObject>();
                WorldObject right = targetRight.GetComponent<WorldObject>();
                if (left == null || right == null) {
                    // Shot structure
                    ChangeState(State.Shooting);
                    m_LeftEye.ShootHit();
                    m_RightEye.ShootHit();
                }
                // Connect! Targets arent in same world yet
                else if (!left.IsBoth() && !right.IsBoth()) {
                    
                    ChangeState(State.ShootingConnected);
                    left.ChangeType(WorldObject.Type.Both);
                    right.ChangeType(WorldObject.Type.Both);
                    m_LeftEye.ShootConnected();
                    m_RightEye.ShootConnected();
                }
                else {
                    //Already in both worlds
                    ChangeState(State.Shooting);
                    m_LeftEye.ShootHit();
                    m_RightEye.ShootHit();
                }
                
            }
            else {
                if (targetLeft == null) {
                    ChangeState(State.Shooting);
                    m_LeftEye.ShootMiss();
                }
                else {
                    ChangeState(State.Shooting);
                    m_LeftEye.ShootHit();
                }

                if (targetRight == null) {
                    ChangeState(State.Shooting);
                    m_RightEye.ShootMiss();
                }
                else  {
                    ChangeState(State.Shooting);
                    m_RightEye.ShootHit();
                }
            }
        }

        private IEnumerator StopShootingAfter(float time) {
            yield return new WaitForSeconds(time);
            ChangeState(State.NotShooting);
        }

        private void ReadInput() {
            if (!m_FireFix) {
                m_FireFix = CrossPlatformInputManager.GetButtonDown("FireFix");
            }
        }

        [System.Serializable]
        public class FixerGunData {
            public Material hitLaser;
  
            public Material connectedLaser;
            public float laserWidthHit;
            public float laserWidthConnect;
            public float laserTimeHit;
            public float laserTimeConnect;

            public ParticleSystem hitParticlesLeft;
            public ParticleSystem hitParticlesRight;

            public ParticleSystem connectingParticlesLeft;
            public ParticleSystem connectingParticlesRight;


            [Range(0, 100)]
            public float fireLength;
        }
    
    }
}
