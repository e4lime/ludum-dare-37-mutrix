using UnityEngine;
using System.Collections;
namespace Lime.PhysicsUtility.Triggers {
    public interface IOnTriggerEnterReceiver {
        void OnTriggerEnter(Collider other);
    }
}