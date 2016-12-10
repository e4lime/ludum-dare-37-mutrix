using UnityEngine;
namespace Lime.PhysicsUtility.Collisions {
    public interface IOnCollisionEnterReceiver {
        void OnCollisionEnter(Collision collision);
    }
}