using UnityEngine;
using System.Collections;
namespace Lime.PhysicsUtility.Triggers {
    public interface IOnTriggerExitReceiver {
        void OnTriggerExit(Collider other);
    }
}
