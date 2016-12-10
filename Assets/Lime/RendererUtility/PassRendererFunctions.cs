using UnityEngine;

/// <summary>
/// Add renderer you want to pass from. Passes to all scripts on given target, not on its childs.
/// </summary>
namespace Lime.RendererUtility {
    public class PassRendererFunctions : MonoBehaviour {


        [Tooltip("Transform with IRenderFunction scripts to pass to")]public Transform target;

        private IRendererFunctions[] allReceivers;

        void Start() {
            allReceivers = target.GetComponents<IRendererFunctions>();
        }

        void OnBecameInvisible() {
            foreach (IRendererFunctions receiver in allReceivers) {
                receiver.OnBecameInvisible();
            }
        }
        void OnBecameVisible() {
            foreach (IRendererFunctions receiver in allReceivers) {
                receiver.OnBecameVisible();
            }
        }
    }
}

