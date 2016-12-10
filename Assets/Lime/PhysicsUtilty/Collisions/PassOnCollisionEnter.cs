using UnityEngine;
using System.Collections;

namespace Lime.PhysicsUtility.Collisions {

    /// <summary>
    /// Why do we have this? shouldnt compund colliders do the same?
    ///     We can use it on on compound colliders to pass to it parent etc!
    /// </summary>
    public class PassOnCollisionEnter : MonoBehaviour {


        public GameObject target;
        //private IOnCollisionEnterReceiver receiver;
        private IOnCollisionEnterReceiver[] allReceivers;

        void Start() {
            //  receiver = target.GetComponent<IOnCollisionEnterReceiver>();

            allReceivers = target.GetComponents<IOnCollisionEnterReceiver>();
        }

        void OnCollisionEnter(Collision collision) {


            foreach (IOnCollisionEnterReceiver receiver in allReceivers) {
                receiver.OnCollisionEnter(collision);
            }
        }
    }
}

